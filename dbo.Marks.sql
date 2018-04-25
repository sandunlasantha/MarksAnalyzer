CREATE TABLE [dbo].[Marks] (
    [Regno]  INT           NOT NULL,
    [Name]   NVARCHAR (50) NULL,
    [Mark]   INT           DEFAULT ((0)) NOT NULL,
    [TestNo] INT           NULL,
    [Rank]   INT           DEFAULT ((0)) NOT NULL,
    [Result] NVARCHAR (50) NULL,
    [ZScore] FLOAT (53)    DEFAULT ((0)) NOT NULL, 
    CONSTRAINT [PK_Marks] PRIMARY KEY ([Regno])
);

