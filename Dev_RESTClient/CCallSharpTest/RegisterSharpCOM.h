#pragma once
class CRegisterSharpCOM
{
public:
	CRegisterSharpCOM(void);
	~CRegisterSharpCOM(void);

	static CString GetCodeBase();
	static BOOL FindFolder(CString folder, BOOL folderOnly, CStringArray& resultList);
	static int  CompareAscending(const void *a, const void *b);
	static int  CompareDescending(const void *a, const void *b);
	static void SortStringArray(CStringArray& csa, BOOL bDescending);
	static BOOL RegisterDotNetAssembly(CString sDllName, int op); //type:0 unregister, 1=register, 2:re-register
};
