CREATE DATABASE [AEMTest];
USE [AEMTest]
GO

/****** Object: Table [dbo].[PlatformInfo] Script Date: 10/18/2021 4:33:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PlatformInfo] (
    [Id]           INT          NOT NULL,
    [PlatformId]   INT          NOT NULL,
    [PlatformName] VARCHAR (50) NULL,
    [UniqueName]   VARCHAR (50) NULL,
    [Latitute]     VARCHAR (15) NULL,
    [Longitude]    VARCHAR (15) NULL,
    [CreatedAt]    DATETIME     NULL,
    [UpdatedAt]    DATETIME     NULL
);


