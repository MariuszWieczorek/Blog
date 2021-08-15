BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Posts]') AND [c].[name] = N'LongDescription');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Posts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Posts] DROP COLUMN [LongDescription];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210813184516_RemoveLongDescriptionInPost', N'5.0.9');
GO

COMMIT;
GO

