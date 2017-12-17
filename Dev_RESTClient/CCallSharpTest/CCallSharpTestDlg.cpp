
// CCallSharpTestDlg.cpp : implementation file
//

#include "stdafx.h"
#include "CCallSharpTest.h"
#include "CCallSharpTestDlg.h"
#include "afxdialogex.h"

#include "RegisterSharpCOM.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#import "RESTClient.tlb" raw_interfaces_only
using namespace RESTClient;

// CCCallSharpTestDlg dialog
CCCallSharpTestDlg::CCCallSharpTestDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CCCallSharpTestDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CCCallSharpTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT_LOG, m_strLog);
}

BEGIN_MESSAGE_MAP(CCCallSharpTestDlg, CDialogEx)
	ON_BN_CLICKED(IDOK, &CCCallSharpTestDlg::OnBnClickedOk)
	ON_BN_CLICKED(IDCANCEL, &CCCallSharpTestDlg::OnBnClickedCancel)
	ON_BN_CLICKED(IDC_BUTTON_STOCK, &CCCallSharpTestDlg::OnBnClickedButtonStock)
	ON_BN_CLICKED(IDC_BUTTON_UPLOAD, &CCCallSharpTestDlg::OnBnClickedButtonUpload)
	ON_BN_CLICKED(IDC_BUTTON_DOWNLOAD, &CCCallSharpTestDlg::OnBnClickedButtonDownload)
END_MESSAGE_MAP()

// CCCallSharpTestDlg message handlers
BOOL CCCallSharpTestDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here
	CRegisterSharpCOM::RegisterDotNetAssembly("RESTClient.dll", 2);

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CCCallSharpTestDlg::OnBnClickedOk()
{
	// Initialize COM.
	HRESULT hr = CoInitialize(NULL);

	// Create the interface pointer.
	CString resultMsg = "Error";
	IHoleLayoutPtr pIHole(__uuidof(HoleLayoutPreview));
	if (pIHole)
	{
		//VARIANT_BOOL result;
		CComBSTR bstr_put("D:\\gitArtisman\\Dev_RESTClient\\design_to_cam.xml");

		//VARIANT_BOOL result = pIHole->CreateHoleLayoutImage(&bstr_put, &result);

		::SysFreeString(bstr_put);
	}

	// Uninitialize COM.
	CoUninitialize();
}

void CCCallSharpTestDlg::OnBnClickedCancel()
{
	// TODO: Add your control notification handler code here
	CDialogEx::OnCancel();
}

void CCCallSharpTestDlg::OnBnClickedButtonStock()
{
	HRESULT hr = CoInitialize(NULL);

	CString resultMsg = "Error";
	IArtCloudPtr pIArtCloud(__uuidof(ArtCloudClass));
	if (pIArtCloud)
	{
		CComBSTR bstrCncType("cnc"); //cnc or saw
		CComBSTR bstrXmlFile("D:\\temp\\NestingData.xml");

		BSTR bstr_msg;
		VARIANT_BOOL result = pIArtCloud->CuttingStock(bstrCncType, bstrXmlFile, &bstr_msg);
		CString errMsg(bstr_msg);
		resultMsg = errMsg;

		::SysFreeString(bstr_msg);
	}

	CoUninitialize();

	if (resultMsg.IsEmpty())
		AfxMessageBox(_T("Success"));
	else
		AfxMessageBox(resultMsg);
}

void CCCallSharpTestDlg::OnBnClickedButtonUpload()
{ //upload.exe /ServerRelPath:"\drawLibs\app"  /LocalFullPath:"D:\test" /ServerProcessor:"Raw Copy"
	HRESULT hr = CoInitialize(NULL);

	CString resultMsg = "Error";
	IArtCloudPtr pIArtCloud(__uuidof(ArtCloudClass));
	if (pIArtCloud)
	{
		CComBSTR bstr_srvPath("/ServerRelPath:\"\\drawLibs\\app\"");
		CComBSTR bstr_clnPath("/LocalFullPath:\"D:\\test\"");
		CComBSTR bstr_processor("/ServerProcessor:\"Raw Copy\"");

		BSTR bstr_msg;
		VARIANT_BOOL result = pIArtCloud->Upload(bstr_srvPath, bstr_clnPath, bstr_processor, &bstr_msg);
		CString errMsg(bstr_msg);
		resultMsg = errMsg;

		::SysFreeString(bstr_msg);
	}

	CoUninitialize();

	if (resultMsg.IsEmpty())
		AfxMessageBox(_T("Success"));
	else
		AfxMessageBox(resultMsg);
}

void CCCallSharpTestDlg::OnBnClickedButtonDownload()
{ //download.exe /ServerRelPath:"\drawLibs\app"  /LocalFullPath:"D:\test\app" /ServerProcessor:"Raw Copy"
	HRESULT hr = CoInitialize(NULL);

	CString resultMsg = "Error";
	IArtCloudPtr pIArtCloud(__uuidof(ArtCloudClass));
	if (pIArtCloud)
	{
		//CComBSTR bstr_srvPath("/ServerRelPath:\"\\drawLibs\\app\"");
		CComBSTR bstr_clnPath("/LocalFullPath:\"D:\\test\\models\\geometry\"");
		//CComBSTR bstr_processor("/ServerProcessor:\"Raw Copy\"");

		CComBSTR bstr_srvPath("/ServerRelPath:\"\\CADM\\SVG\\geometry\"");
		CComBSTR bstr_processor("/ServerProcessor:\"CADM Models\"");

		BSTR bstr_msg;
		VARIANT_BOOL result = pIArtCloud->Download(bstr_srvPath, bstr_clnPath, bstr_processor, &bstr_msg);
		CString errMsg(bstr_msg);
		resultMsg = errMsg;

		::SysFreeString(bstr_msg);
	}

	CoUninitialize();

	if (resultMsg.IsEmpty())
		AfxMessageBox(_T("Success"));
	else
		AfxMessageBox(resultMsg);
}
