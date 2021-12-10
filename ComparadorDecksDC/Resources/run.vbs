'---------------------------------------------------
' Objetos para manipula��o de arquivos e shell
Set objShell = CreateObject("Wscript.Shell")
Set objFileSystem = CreateObject("Scripting.FileSystemObject")

'---------------------------------------------------
'Pega informa��es sobre a origem
strPathAtual = left(WScript.ScriptFullName,(Len(WScript.ScriptFullName))-(len(WScript.ScriptName)))
strEsteArquivo = WScript.ScriptName
strConfiguracaoScript = strPathAtual & "run.ini"
Set objPasta = objFileSystem.GetFolder(strPathAtual)


'---------------------------------------------------
'Extrai as informa�oes de configura��o do arquivo run.ini
strCfgExecutar = ReadIni(strConfiguracaoScript, "run", "executar")
strCfgTitulo = ReadIni(strConfiguracaoScript, "run", "titulo")
strCfgAmbiente = ReadIni(strConfiguracaoScript, "run", "ambiente") 'DEV, QA, PROD
strCfgIcone = ReadIni(strConfiguracaoScript, "run", "icone")

strStatusExecucao = Executar()

Select Case LCase(strStatusExecucao)
	Case "ok"
	Case Else
		MsgBox "[" & strCfgTitulo & "]" & vbCrLf & vbCrLf & strStatusExecucao
End Select

'-----------------------------------------
'Fun��o principal

Function Executar ()

	On Error Resume Next

	'---------------------------------------------------
	' Cria a pasta CompassApp no computador do usu�rio, caso n�o exista
	Select Case LCase(strCfgAmbiente)
		Case "dev"
			strBaseDestino = "C:\Users\" & objShell.ExpandEnvironmentStrings("%USERNAME%") & "\CompassApp_Dev\"
		Case "qa"
			strBaseDestino = "C:\Users\" & objShell.ExpandEnvironmentStrings("%USERNAME%") & "\CompassApp_QA\"
		Case Else
			strBaseDestino = "C:\Users\" & objShell.ExpandEnvironmentStrings("%USERNAME%") & "\CompassApp\"
	End Select

	If Not objFileSystem.FolderExists(strBaseDestino) Then
		objFileSystem.CreateFolder (strBaseDestino)
	End If


	'---------------------------------------------------
	' Monta o path do destino
	strPathDestino = strBaseDestino & objPasta.name & "\"

	'---------------------------------------------------
	' Pega as vers�es da instala��o local e do que est� dispon�vel na rede
	' Considera -1 caso n�o esteja instalado localmente
	If objFileSystem.FileExists(strPathDestino & "\run.ini") Then
		strLocalVersion = ReadIni(strPathDestino & "\run.ini", "versao", "versao")
	Else
		strLocalVersion = "-1"
	End If
	StrCurrentVersion = ReadIni(strConfiguracaoScript, "versao", "versao")
	
	'---------------------------------------------------
	' Se as vers�es (local/remota) forem diferentes, atualiza
	' a instala��o local. Se n�o, vai direto para a execu��o
	' e cria��o de atalhos
	If strLocalVersion <> strCurrentVersion And strLocalVersion <> "" And strCurrentVersion <> "" Then

		'---------------------------------------------------
		'Prepara o destino para receber os arquivos.
		'Se a pasta n�o existir, cria.
		'Se existir, limpa

		If Not objFileSystem.FolderExists(strPathDestino) Then
			'Cria a pasta de destino, pois n�o existe.
			objFileSystem.CreateFolder (strPathDestino)
		Else
			'dim strLocalVersion as string
			'dim strCurrentVersion as string
							
			'O destino existe. Vai limpar conforme descrito acima.
			Set objPastaDestino = objFileSystem.GetFolder(strBaseDestino & objPasta.name)
			
			for each file in objPastaDestino.Files
				
				strNomeDoArquivo = LCase(file.name)
				
				'Definir aqui os arquivos a ignorar na exclus�o
				If	Not IsItemIgnorado(strNomeDoArquivo) Then
					objFileSystem.DeleteFile strPathDestino & file.name, True
					
					If Err.Number <> 0 Then
						Executar =	"Se o programa estiver aberto, feche-o antes de executar o atalho. Caso contr�rio, contacte o suporte."  & vbCrLf & vbCrLf & _
									"ERRO: N�o foi poss�vel apagar o arquivo " & strNomeDoArquivo &vbCrLf & _
									"MOTIVO: " & Err.Description
						Exit Function
					End If
					
				End If
				
			next
			
			for each subfolder in objPastaDestino.SubFolders
			
				'Definir aqui subpastas a ignorar na exclus�o
				 If Not IsItemIgnorado(subfolder.name) Then
					objFileSystem.DeleteFolder (subfolder)
					
					If Err.Number <> 0 Then
						Executar =	"Se o programa estiver aberto, feche-o antes de executar o atalho. Caso contr�rio, contacte o suporte."  & vbCrLf & vbCrLf & _
									"ERRO: N�o foi poss�vel apagar a pasta " & subfolder.name &vbCrLf & _
									"MOTIVO: " & Err.Description
						Exit Function
					End If
					
				 End If
				 
			next
			
		End If
				
				
		'---------------------------------------------------
		'Copia os arquivos do programa para o destino,

		for each file in objPasta.Files
			strExtensao = LCase(objFileSystem.GetExtensionName(objPasta & file.name))
			'Definir aqui arquivos para n�o copiar para o usu�rio
			If	file.name <> strEsteArquivo And _
				Not IsItemIgnorado(file.name) _
			Then
				objFileSystem.CopyFile file, strPathDestino, True
				
				If Err.Number <> 0 Then
					Executar =	"N�o foi poss�vel atualizar o programa. Contacte o suporte."  & vbCrLf & vbCrLf & _
								"ERRO: N�o foi poss�vel copiar o arquivo " & file.name & subfolder.name &vbCrLf & _
								"MOTIVO: " & Err.Description
					Exit Function
				End If
				
			End If
		next

		for each subfolder in objPasta.SubFolders
			'Definir aqui subpastas para n�o copiar para o usu�rio
			If Not IsItemIgnorado(subfolder.name) Then
				objFileSystem.CopyFolder subfolder, strPathDestino, True
				
				If Err.Number <> 0 Then
					Executar =	"N�o foi poss�vel atualizar o programa. Contacte o suporte."  & vbCrLf & vbCrLf & _
								"ERRO: N�o foi poss�vel copiar a pasta " & subfolder.name & subfolder.name &vbCrLf & _
								"MOTIVO: " & Err.Description
					Exit Function
				End If
				
			End If
		next

	End If
		
	'---------------------------------------------------
	'Ap�s a copia, executa a aplica��o
	strArquivoParaChamar = ReadIni(strConfiguracaoScript, "run", "executar")
	objShell.Run """" & strPathDestino & strCfgExecutar & """"

	'---------------------------------------------------
	'Se n�o existia atalho no menu iniciar, cria um

	Dim strBaseProgramas

	Select Case LCase(strCfgAmbiente)
		Case "dev"
			strBaseProgramas = objShell.SpecialFolders("Programs") & "\Compass - DEV"
		Case "qa"
			strBaseProgramas = objShell.SpecialFolders("Programs") & "\Compass - QA"
		Case Else
			strBaseProgramas = objShell.SpecialFolders("Programs") & "\Compass"
	End Select

	If Not objFileSystem.FolderExists(strBaseProgramas) Then
		objFileSystem.CreateFolder (strBaseProgramas)
	End If

	strArquivoAtalho = strBaseProgramas & "\" & strCfgTitulo & ".lnk"

	Set objAtalho = objShell.CreateShortcut (strArquivoAtalho)
	objAtalho.TargetPath = strPathAtual & "run.vbs"
	objAtalho.WorkingDirectory = strPathAtual

	If Trim(strCfgIcone) <> "" And objFileSystem.FileExists(strPathDestino & "\" & strCfgIcone) Then
		objAtalho.IconLocation = strPathDestino & "\" & strCfgIcone
	Else
		objAtalho.IconLocation = strPathDestino & strCfgExecutar & ",0"
	End If

	objAtalho.Save

	If LCase(strCfgAmbiente) <> "dev" And LCase(strCfgAmbiente) <> "qa" Then
		'Est� executando em produ��o. Atualiza o link no Desktop
		strPastaDesktop = objShell.SpecialFolders("Desktop") & "\"
		objFileSystem.CopyFile strArquivoAtalho, strPastaDesktop
	End If
	
	Executar = "ok"
End Function

'-----------------------------------------------------------------
'Fun��o que diz se o arquivo deve ser ignorado, olhando o
'par�metro "ignorar" do arquivo run.ini
Function IsItemIgnorado (strItem)
	strIgnorar = ";" & LCase(ReadIni(strConfiguracaoScript, "run", "ignorar")) & ";"
	If Not InStr (1, strIgnorar, ";" & strItem & ";") >= 1 Then
		IsItemIgnorado = False
		Exit Function
	Else
		IsItemIgnorado = True
		Exit Function
	End If
End Function


'-----------------------------------------------------------------
'Fun��o para ler arquivos do tipo INI
'http://www.robvanderwoude.com/vbstech_files_ini.php
Function ReadIni( myFilePath, mySection, myKey )
    ' This function returns a value read from an INI file
    '
    ' Arguments:
    ' myFilePath  [string]  the (path and) file name of the INI file
    ' mySection   [string]  the section in the INI file to be searched
    ' myKey       [string]  the key whose value is to be returned
    '
    ' Returns:
    ' the [string] value for the specified key in the specified section
    '
    ' CAVEAT:     Will return a space if key exists but value is blank
    '
    ' Written by Keith Lacelle
    ' Modified by Denis St-Pierre and Rob van der Woude

    Const ForReading   = 1
    Const ForWriting   = 2
    Const ForAppending = 8

    Dim intEqualPos
    Dim objFSO, objIniFile
    Dim strFilePath, strKey, strLeftString, strLine, strSection

    Set objFSO = CreateObject( "Scripting.FileSystemObject" )

    ReadIni     = ""
    strFilePath = Trim( myFilePath )
    strSection  = Trim( mySection )
    strKey      = Trim( myKey )

    If objFSO.FileExists( strFilePath ) Then
        Set objIniFile = objFSO.OpenTextFile( strFilePath, ForReading, False )
        Do While objIniFile.AtEndOfStream = False
            strLine = Trim( objIniFile.ReadLine )

            ' Check if section is found in the current line
            If LCase( strLine ) = "[" & LCase( strSection ) & "]" Then
                strLine = Trim( objIniFile.ReadLine )

                ' Parse lines until the next section is reached
                Do While Left( strLine, 1 ) <> "["
                    ' Find position of equal sign in the line
                    intEqualPos = InStr( 1, strLine, "=", 1 )
                    If intEqualPos > 0 Then
                        strLeftString = Trim( Left( strLine, intEqualPos - 1 ) )
                        ' Check if item is found in the current line
                        If LCase( strLeftString ) = LCase( strKey ) Then
                            ReadIni = Trim( Mid( strLine, intEqualPos + 1 ) )
                            ' In case the item exists but value is blank
                            If ReadIni = "" Then
                                ReadIni = " "
                            End If
                            ' Abort loop when item is found
                            Exit Do
                        End If
                    End If

                    ' Abort if the end of the INI file is reached
                    If objIniFile.AtEndOfStream Then Exit Do

                    ' Continue with next line
                    strLine = Trim( objIniFile.ReadLine )
                Loop
            Exit Do
            End If
        Loop
        objIniFile.Close
    Else
        WScript.Echo strFilePath & " doesn't exists. Exiting..."
        Wscript.Quit 1
    End If
End Function
