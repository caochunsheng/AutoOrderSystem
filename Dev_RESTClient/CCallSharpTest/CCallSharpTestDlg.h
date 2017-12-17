
// CCallSharpTestDlg.h : header file
//

#pragma once


// CCCallSharpTestDlg dialog
class CCCallSharpTestDlg : public CDialogEx
{
// Construction
public:
	CCCallSharpTestDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_CCALLSHARPTEST_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	afx_msg void OnBnClickedCancel();
	CString m_strLog;
	afx_msg void OnBnClickedButtonStock();
	afx_msg void OnBnClickedButtonUpload();
	afx_msg void OnBnClickedButtonDownload();
};
