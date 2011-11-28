﻿using System;

namespace RestSharp.Authenticators.OAuth
{
#if !SILVERLIGHT && !WINDOWS_PHONE
	[Serializable]
#endif
	public enum OAuthType
	{
		RequestToken,
		AccessToken,
		ProtectedResource,
		ClientAuthentication
	}
}