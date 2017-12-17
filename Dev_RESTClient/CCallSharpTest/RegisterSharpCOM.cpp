#include "stdafx.h"
#include "io.h"
#include "direct.h"
#include "RegisterSharpCOM.h"

CRegisterSharpCOM::CRegisterSharpCOM(void)
{
}

CRegisterSharpCOM::~CRegisterSharpCOM(void)
{
}

CString CRegisterSharpCOM::GetCodeBase()
{
	CString strRunPath = "";
	char	szIniPath[MAX_PATH];

	DWORD dwResult = GetModuleFileName(NULL, szIniPath, sizeof(szIniPath));
	if (0 != dwResult)
	{
		strRunPath = szIniPath;
		strRunPath.Replace("/", "\\");
		int index = strRunPath.ReverseFind('\\');
		if (index != -1)
			strRunPath = strRunPath.Left(index+1);
	}
	return strRunPath;
}

BOOL CRegisterSharpCOM::FindFolder(CString folder, BOOL folderOnly, CStringArray& resultList)
{
	resultList.RemoveAll();		
	resultList.FreeExtra();

	CString strRoot = folder;
	strRoot.Replace("/", "\\");
	if (strRoot.Right(1) != "\\")
		strRoot += "\\";

	long	hFile = -1;
	BOOL	bFirst = TRUE;
    struct	_finddata_t c_file;
	while(TRUE)
	{
		if (bFirst)
		{
			bFirst = FALSE;
			if ((hFile = _findfirst(strRoot+"*.*", &c_file)) == -1L)
				return FALSE;
		}
		else
		{
            if (_findnext(hFile, &c_file) == -1L)
				break;
		}

		if (c_file.attrib & _A_SUBDIR)
		{
			if (folderOnly && (strcmp(c_file.name, ".") != 0) && (strcmp(c_file.name, "..") != 0))
				resultList.Add(c_file.name);
		}
		else
		{
			resultList.Add(c_file.name);
		}
	}

	if (hFile > 0)
		_findclose( hFile );

	return (resultList.GetCount() > 0);
}

int CRegisterSharpCOM::CompareAscending(const void *a, const void *b)
{
  CString *pA = (CString*)a;
  CString *pB = (CString*)b;
  return (pA->Compare(*pB));
}

int CRegisterSharpCOM::CompareDescending(const void *a, const void *b)
{
  CString *pA = (CString*)a;
  CString *pB = (CString*)b;
  return (-1 * (pA->Compare(*pB)));
}

void CRegisterSharpCOM::SortStringArray(CStringArray& csa, BOOL bDescending)
{
  int iArraySize = csa.GetSize();
  if (iArraySize <= 0)
     return;

  int iCSSize = sizeof (CString*);
  void* pArrayStart = (void *)&csa[0];

  if (bDescending)
     qsort (pArrayStart, iArraySize, iCSSize, CompareDescending);
  else
     qsort (pArrayStart, iArraySize, iCSSize, CompareAscending);
}

BOOL CRegisterSharpCOM::RegisterDotNetAssembly(CString sDllName, int op) //type:0 unregister, 1=register, 2:re-register
{	// This uses the redistributable file regasm.exe to register Dot Net Assemblies.
	BOOL bReturn;

	CString sExeDir = GetCodeBase();
	TCHAR buff[MAX_PATH];
	GetWindowsDirectory(buff, MAX_PATH);

	CString strParam;
	strParam.Format(" \"%s%s\"", sExeDir, sDllName);
	if (op == 0)
		strParam.Format("/u \"%s%s\"", sExeDir, sDllName);
	else if (op == 2)
		strParam.Format("/codebase \"%s%s\"", sExeDir, sDllName);

	CString sFrameworkFolder;
	sFrameworkFolder.Format("%s\\Microsoft.NET\\Framework", buff);	//RegAsm.exe

	CStringArray frameworkVersion;
	CString strFrameworkDetected = sFrameworkFolder+"\\";
	if (!FindFolder(sFrameworkFolder+"\\", TRUE, frameworkVersion))
	{
		strFrameworkDetected = sFrameworkFolder+"64\\";
		if (!FindFolder(sFrameworkFolder+"64\\", TRUE, frameworkVersion))
			return FALSE;
	}

	SortStringArray(frameworkVersion, TRUE);

	for(int i = 0; i < frameworkVersion.GetCount(); i ++)
	{
		CString sPath;
		sPath.Format("%s%s\\RegAsm.exe", strFrameworkDetected, frameworkVersion[i]);	//RegAsm.exe
		if (access(sPath, 0) >= 0)
		{
			SHELLEXECUTEINFO ShExecInfo = {0};
			ShExecInfo.cbSize = sizeof(SHELLEXECUTEINFO);
			ShExecInfo.fMask = SEE_MASK_NOCLOSEPROCESS;
			ShExecInfo.hwnd = NULL;
			ShExecInfo.lpVerb = "open";
			ShExecInfo.lpFile = sPath;
			ShExecInfo.lpParameters = strParam;
			ShExecInfo.lpDirectory = sExeDir;
			ShExecInfo.nShow = SW_HIDE;
			ShExecInfo.hInstApp = NULL;      
			bReturn = ShellExecuteEx(&ShExecInfo);
			WaitForSingleObject(ShExecInfo.hProcess,70000);
			break;
		}
	}
	return bReturn;
}
