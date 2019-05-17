USE EMMA
GO
SET IDENTITY_INSERT [dbo].[JobConfig] ON
INSERT INTO [dbo].[JobConfig] ([Id], [SysStart], [SysEnd], [SysUser], [SysUserNext], [JobName], [ConnectionString], [ViewName], [SenderAddress], [SenderName], [BodyTemplate], [SubjectTemplate], [DataRateDays], [MessageResendLimit]) VALUES (1, N'2018-01-01 00:00:00', N'9999-12-31 00:00:00', N'Frank.Linden@ct.gov', NULL, N'MedicaidCoverageChange', N'Server=(localdb)\\MSSQLLocalDB;Database=Camris;Trusted_Connection=True;MultipleActiveResultSets=true', N'vwMedicaidCoverageChange', N'Frank.Linden@ct.gov', N'Frank''s test account', N'The following message in being sent in regard to Individual: {ddsnum}\n {Message}', N'{Subject}', 1, 2)
SET IDENTITY_INSERT [dbo].[JobConfig] OFF