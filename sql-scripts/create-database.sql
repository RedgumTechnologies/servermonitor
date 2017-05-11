CREATE TABLE [dbo].[tbl_MON_Server](
	[IDServer] [int] IDENTITY(1,1) NOT NULL,
	[GIDServer] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Domain] [nvarchar](100) NOT NULL,
	[UpdateDateTime] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_tbl_MON_Server] PRIMARY KEY CLUSTERED 
(
	[IDServer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[tbl_MON_Server] ADD  CONSTRAINT [DF_tbl_MON_Server_GIDServer]  DEFAULT (newid()) FOR [GIDServer]
GO

ALTER TABLE [dbo].[tbl_MON_Server] ADD  CONSTRAINT [DF_tbl_MON_Server_Name]  DEFAULT ('') FOR [Name]
GO

ALTER TABLE [dbo].[tbl_MON_Server] ADD  CONSTRAINT [DF_tbl_MON_Server_Domain]  DEFAULT ('') FOR [Domain]
GO

ALTER TABLE [dbo].[tbl_MON_Server] ADD  CONSTRAINT [DF_tbl_MON_Server_UpdateDateTime]  DEFAULT (getdate()) FOR [UpdateDateTime]
GO


CREATE TABLE [dbo].[tbl_MON_ServerData](
	[IDServerData] [int] IDENTITY(1,1) NOT NULL,
	[GIDServerData] [uniqueidentifier] NOT NULL,
	[lnkServerID] [int] NOT NULL,
	[DataKey] [nvarchar](50) NOT NULL,
	[Data] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_tbl_MON_ServerData] PRIMARY KEY CLUSTERED 
(
	[IDServerData] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[tbl_MON_ServerData] ADD  CONSTRAINT [DF_tbl_MON_ServerData_GIDServerData]  DEFAULT (newid()) FOR [GIDServerData]
GO

ALTER TABLE [dbo].[tbl_MON_ServerData] ADD  CONSTRAINT [DF_tbl_MON_ServerData_lnkServerID]  DEFAULT ((0)) FOR [lnkServerID]
GO

ALTER TABLE [dbo].[tbl_MON_ServerData] ADD  CONSTRAINT [DF_Table_1_Key]  DEFAULT ('') FOR [DataKey]
GO

ALTER TABLE [dbo].[tbl_MON_ServerData] ADD  CONSTRAINT [DF_tbl_MON_ServerData_Data]  DEFAULT ('') FOR [Data]
GO


CREATE TABLE [dbo].[tbl_MON_ServerDataAggregate](
	[IDServerDataAggregate] [int] IDENTITY(1,1) NOT NULL,
	[GIDServerDataAggregate] [uniqueidentifier] NOT NULL,
	[lnkServerID] [int] NOT NULL,
	[DataKey] [nvarchar](50) NOT NULL,
	[Data] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_tbl_MON_ServerDataAggregate] PRIMARY KEY CLUSTERED 
(
	[IDServerDataAggregate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[tbl_MON_ServerDataAggregate] ADD  CONSTRAINT [DF_tbl_MON_ServerDataAggregate_GIDServerData]  DEFAULT (newid()) FOR [GIDServerDataAggregate]
GO

ALTER TABLE [dbo].[tbl_MON_ServerDataAggregate] ADD  CONSTRAINT [DF_tbl_MON_ServerDataAggregate_lnkServerID]  DEFAULT ((0)) FOR [lnkServerID]
GO

ALTER TABLE [dbo].[tbl_MON_ServerDataAggregate] ADD  CONSTRAINT [DF_tbl_MON_ServerDataAggregate_DataKey]  DEFAULT ('') FOR [DataKey]
GO

ALTER TABLE [dbo].[tbl_MON_ServerDataAggregate] ADD  CONSTRAINT [DF_tbl_MON_ServerDataAggregate_Data]  DEFAULT ('') FOR [Data]
GO

CREATE TABLE [dbo].[tbl_MON_Setting](
	[IDSetting] [int] IDENTITY(1,1) NOT NULL,
	[GIDSetting] [uniqueidentifier] NOT NULL,
	[DataKey] [nvarchar](50) NOT NULL,
	[Data] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_tbl_MON_Setting] PRIMARY KEY CLUSTERED 
(
	[IDSetting] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[tbl_MON_Setting] ADD  CONSTRAINT [DF_tbl_MON_Setting_GIDSetting]  DEFAULT (newid()) FOR [GIDSetting]
GO

ALTER TABLE [dbo].[tbl_MON_Setting] ADD  CONSTRAINT [DF_tbl_MON_Setting_DataKey]  DEFAULT ('') FOR [DataKey]
GO

ALTER TABLE [dbo].[tbl_MON_Setting] ADD  CONSTRAINT [DF_tbl_MON_Setting_Data]  DEFAULT ('') FOR [Data]
GO


CREATE TYPE [dbo].[utt_MON_ServerData] AS TABLE(
	[IDServer] [int] NOT NULL,
	[DataKey] [nvarchar](50) NULL,
	[Data] [nvarchar](max) NULL
)
GO


CREATE PROCEDURE [dbo].[usp_MON_Server_Get](
	@pServerName NVARCHAR(50))
AS
BEGIN

	SELECT * FROM tbl_MON_Server
	WHERE Name = @pServerName
END

GO

CREATE PROCEDURE [dbo].[usp_MON_Server_List]
AS
BEGIN
	
	SELECT IDServer, GIDServer, Name, Domain, UpdateDateTime
	FROM tbl_MON_Server
	ORDER BY Domain, Name
END

GO

CREATE PROCEDURE [dbo].[usp_MON_ServerData_List]
AS
BEGIN
	
	SELECT IDServer = lnkServerID, Name, Domain, DataKey, Data
	FROM tbl_MON_Server SVR
		INNER JOIN tbl_MON_ServerData SDT ON SDT.lnkServerID = SVR.IDServer
	ORDER BY Domain, Name
END

GO

CREATE PROCEDURE [dbo].[usp_MON_ServerData_List_ForServer] (
	@pIDServer INT
	)
AS
BEGIN
	
	SELECT IDServer = lnkServerID, Name, Domain, DataKey, Data
	FROM tbl_MON_Server SVR
		INNER JOIN tbl_MON_ServerData SDT ON SDT.lnkServerID = SVR.IDServer
	WHERE SDT.lnkServerID = @pIDServer
	ORDER BY Domain, Name
END

GO

CREATE PROCEDURE [dbo].[usp_MON_ServerDataAggregate_List]
AS
BEGIN
	
	SELECT IDServer = lnkServerID, Name, Domain, DataKey, Data
	FROM tbl_MON_Server SVR
		INNER JOIN tbl_MON_ServerDataAggregate SDA ON SDA.lnkServerID = SVR.IDServer
	ORDER BY Domain, Name
END

GO


CREATE PROCEDURE [dbo].[usp_MON_ServerDataAggregate_List_ForServer] (
	@pIDServer INT
	)
AS
BEGIN
	
	SELECT IDServer = lnkServerID, Name, Domain, DataKey, Data
	FROM tbl_MON_Server SVR
		INNER JOIN tbl_MON_ServerDataAggregate SDA ON SDA.lnkServerID = SVR.IDServer
	WHERE SDA.lnkServerID = @pIDServer
	ORDER BY Domain, Name
END

GO



CREATE PROCEDURE [dbo].[usp_MON_ServerInfo_Write](
	@pServerName NVARCHAR(50),
	@pDomainName NVARCHAR(100),
	@pUpdateDateTime DATETIMEOFFSET(7),
	@pDataAggregate utt_MON_ServerData READONLY,
	@pData utt_MON_ServerData READONLY
)
AS
BEGIN
	


	-- Insert the header if needed, and retrieve the ID
	DECLARE @IDServer INT = 0

	SELECT @IDServer = IDServer 
		FROM tbl_MON_Server
		WHERE Name = @pServerName

	IF (@IDServer = 0)
		BEGIN
			INSERT INTO tbl_MON_Server (Name, Domain, UpdateDateTime)
			VALUES (@pServerName, @pDomainName, @pUpdateDateTime)

			SET @IDServer = SCOPE_IDENTITY()
		END
	ELSE
		BEGIN
			UPDATE tbl_MON_Server 
				SET Name = @pServerName,
				Domain = @pDomainName, 
				UpdateDateTime = @pUpdateDateTime
			WHERE IDServer  = @IDServer

		END

	-- Update the @pData table type to set the ID value
	DECLARE @Data TABLE (IDServer INT, DataKey NVARCHAR(50), Data NVARCHAR(MAX))
	DECLARE @DataAggregate TABLE (IDServer INT, DataKey NVARCHAR(50), Data NVARCHAR(MAX))

	INSERT INTO @Data
	SELECT @IDServer, DataKey, Data
		FROM @pData


	INSERT INTO @DataAggregate
	SELECT @IDServer, DataKey, Data
		FROM @pDataAggregate

	-- Merge the data blocks into the table
	-- DECLARE @ID INT = 0
	-- DECLARE @DataKey NVARCHAR(50) = ''

	MERGE dbo.tbl_MON_ServerData AS target
    USING (SELECT IDServer, DataKey, Data FROM @Data) AS source (IDServer, DataKey, Data)
    ON (target.DataKey = source.DataKey AND target.lnkServerID =  source.IDServer)
    WHEN MATCHED THEN 
        UPDATE SET Data = source.Data
	WHEN NOT MATCHED THEN	
	    INSERT (lnkServerID, DataKey, Data)
	    VALUES (source.IDServer, source.DataKey, source.Data)
	    OUTPUT $action, inserted.*;

	MERGE dbo.tbl_MON_ServerDataAggregate AS target
    USING (SELECT IDServer, DataKey, Data FROM @DataAggregate) AS source (IDServer, DataKey, Data)
    ON (target.DataKey = source.DataKey AND target.lnkServerID =  source.IDServer)
    WHEN MATCHED THEN 
        UPDATE SET Data = source.Data
	WHEN NOT MATCHED THEN	
	    INSERT (lnkServerID, DataKey, Data)
	    VALUES (source.IDServer, source.DataKey, source.Data)
	    OUTPUT $action, inserted.*;

END

GO


CREATE PROCEDURE [dbo].[usp_MON_Setting_List]
AS
BEGIN
	
	SELECT IDSetting,GIDSetting,DataKey,Data
	FROM  tbl_MON_Setting
	ORDER BY DataKey,Data
END


GO






