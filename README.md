# ROADtoken
ROADtoken is a tool that uses the `BrowserCore.exe` binary to obtain a cookie that can be used with SSO and Azure AD. It mimics (to an extend) the way in which Chrome requests SSO cookies with the Windows 10 accounts extension. For more info, read the [blog](https://dirkjanm.io/abusing-azure-ad-sso-with-the-primary-refresh-token/) about it. At the moment you'll need to compile the binary yourself using Visual Studio (tested with 2017 and 2019). It requires .NET 4.5 but should work with older versions as well.