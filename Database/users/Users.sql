USE [HiringTracker]
GO
/****** Object:  User [hrt_svc_api]    Script Date: 8/21/2021 11:25:34 AM ******/
CREATE USER [hrt_svc_api] FOR LOGIN [hrt_svc_api] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [hrt_test_account]    Script Date: 8/21/2021 11:25:34 AM ******/
CREATE USER [hrt_test_account] FOR LOGIN [hrt_test_account] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [hrt_svc_api]
GO
ALTER ROLE [db_owner] ADD MEMBER [hrt_test_account]
GO
