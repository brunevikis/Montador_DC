using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ToolBox;

namespace ToolBox.Helpers {
    public static class DataGridViewHelper {

        public static void ApplyDefaults(this DataGridView p_objDataGridView) {
            // Configuração default dos grids controlados pela classe.
            p_objDataGridView.AllowUserToAddRows = false;
            p_objDataGridView.AllowUserToDeleteRows = false;
            p_objDataGridView.AllowUserToOrderColumns = true;
            p_objDataGridView.AllowUserToResizeColumns = true;
            p_objDataGridView.AllowUserToResizeRows = false;
            p_objDataGridView.CellPainting += new DataGridViewCellPaintingEventHandler(p_objDataGridViewControlado_CellPainting);
        }

        public static void ApplyFilters(this DataGridView dataGridView) {
            dataGridView.DataSourceChanged += new EventHandler(dataGridView1_BindingContextChanged);
            dataGridView1_BindingContextChanged(dataGridView, null);
        }

        private static void dataGridView1_BindingContextChanged(object sender, EventArgs e) {
            // Continue only if the data source has been set.
            var dataGridView1 = sender as DataGridView;

            if (dataGridView1.DataSource == null) {
                return;
            }

            // Add the AutoFilter header cell to each column.
            foreach (DataGridViewColumn col in dataGridView1.Columns) {
                col.HeaderCell = new
                    DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
            }
        }

        private static void dataGridView1_KeyDown(object sender, KeyEventArgs e) {
            var dataGridView1 = sender as DataGridView;

            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)) {
                DataGridViewAutoFilterColumnHeaderCell filterCell =
                    dataGridView1.CurrentCell.OwningColumn.HeaderCell as
                    DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell != null) {
                    filterCell.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }
        static void p_objDataGridViewControlado_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) {
                if (e.RowIndex % 2 == 0) {
                    e.CellStyle.BackColor = Color.White;
                } else {
                    e.CellStyle.BackColor = Color.Gainsboro;
                }
            }
        }

        public static void ApplySelectFeature(DataGridView p_objDataGridView) {
            if (p_objDataGridView.ContextMenuStrip == null) {
                p_objDataGridView.ContextMenuStrip = new ContextMenuStrip();
            } else {
                p_objDataGridView.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            }

            p_objDataGridView.ContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(
                (System.ComponentModel.CancelEventHandler)delegate(object sender, System.ComponentModel.CancelEventArgs e) {

                ContextMenuStrip objMenu = sender as ContextMenuStrip;
                if (objMenu == null) return;

                DataGridView objGrid = objMenu.SourceControl as DataGridView;
                if (objGrid == null) return;

                bool blnHabilitaItensMenu = (objGrid.Rows.Count != 0);

                ToolStripItem objItemHabilitar;
                objItemHabilitar = objGrid.ContextMenuStrip.Items["select_all"];
                if (objItemHabilitar != null) objItemHabilitar.Enabled = blnHabilitaItensMenu;
                objItemHabilitar = objGrid.ContextMenuStrip.Items["select_none"];
                if (objItemHabilitar != null) objItemHabilitar.Enabled = blnHabilitaItensMenu;
                objItemHabilitar = objGrid.ContextMenuStrip.Items["select_inv"];
                if (objItemHabilitar != null) objItemHabilitar.Enabled = blnHabilitaItensMenu;

            }
            );

            ToolStripMenuItem item;

            // -------------- SELECIONAR TODOS -----------------
            item = new ToolStripMenuItem("Selecionar todos");
            item.Name = "select_all";
            item.Click += (EventHandler)delegate(object sender, EventArgs e) {
                ToolStripMenuItem objItem = sender as ToolStripMenuItem;
                if (objItem == null) return;
                ContextMenuStrip objMenu = objItem.GetCurrentParent() as ContextMenuStrip;
                if (objMenu == null) return;
                DataGridView objGrid = objMenu.SourceControl as DataGridView;
                if (objGrid == null) return;
                foreach (DataGridViewRow row in objGrid.Rows) {
                    row.Cells["select"].Value = true;
                }
                objGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                objGrid.RefreshEdit();
            };
            p_objDataGridView.ContextMenuStrip.Items.Add(item);

            // -------------- LIMPAR SELEÇÃO -----------------
            item = new ToolStripMenuItem("Limpar seleção");
            item.Name = "select_none";
            item.Click += (EventHandler)delegate(object sender, EventArgs e) {
                ToolStripMenuItem objItem = sender as ToolStripMenuItem;
                if (objItem == null) return;
                ContextMenuStrip objMenu = objItem.GetCurrentParent() as ContextMenuStrip;
                if (objMenu == null) return;
                DataGridView objGrid = objMenu.SourceControl as DataGridView;
                if (objGrid == null) return;
                foreach (DataGridViewRow row in objGrid.Rows) {
                    row.Cells["select"].Value = false;
                }
                objGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                objGrid.RefreshEdit();
            };
            p_objDataGridView.ContextMenuStrip.Items.Add(item);

            // -------------- INVERTER SELEÇÃO -----------------
            item = new ToolStripMenuItem("Inverter seleção");
            item.Name = "select_inv";
            item.Click += (EventHandler)delegate(object sender, EventArgs e) {
                ToolStripMenuItem objItem = sender as ToolStripMenuItem;
                if (objItem == null) return;
                ContextMenuStrip objMenu = objItem.GetCurrentParent() as ContextMenuStrip;
                if (objMenu == null) return;
                DataGridView objGrid = objMenu.SourceControl as DataGridView;
                if (objGrid == null) return;
                foreach (DataGridViewRow row in objGrid.Rows) {
                    if (row.Cells["select"].Value is bool) {
                        row.Cells["select"].Value = !(bool)row.Cells["select"].Value;
                    }
                }
                objGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                objGrid.RefreshEdit();
            };
            p_objDataGridView.ContextMenuStrip.Items.Add(item);

        }

        public static DataGridViewPainter GetPainter(object sender, DataGridViewCellPaintingEventArgs e) {
            return new DataGridViewPainter(sender, e);
        }

        public class DataGridViewPainter {

            DataGridView m_objSender;
            DataGridViewCellPaintingEventArgs m_objE;

            public DataGridView sender { get { return this.m_objSender; } }
            public DataGridViewCellPaintingEventArgs e { get { return this.m_objE; } }

            public int Row {
                get { return m_objE.RowIndex; }
            }

            public int Col {
                get { return m_objE.ColumnIndex; }
            }

            public DataGridViewPainter(object sender, DataGridViewCellPaintingEventArgs e) {
                m_objSender = sender as DataGridView;
                m_objE = e;
            }

            public DataGridViewPainter RowIcon(Image p_objImage) {
                if (e.ColumnIndex == -1) {
                    e.PaintBackground(e.ClipBounds, false);
                    e.Graphics.DrawImage(p_objImage, e.CellBounds.Left + 15, e.CellBounds.Top + (e.CellBounds.Height - p_objImage.Height) / 2);
                    e.Paint(e.ClipBounds, DataGridViewPaintParts.ContentBackground);
                    e.Handled = true;
                }
                return this;
            }

            public DataGridViewPainter RowColor(Color p_objColor) {
                e.CellStyle.ForeColor = p_objColor;
                return this;
            }

        }

    }



}
