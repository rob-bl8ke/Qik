/* ===================================================================================================
Generated:
	@{filePath}

	sqlcmd -S %SQLSERVER% %CREDENTIALS% -d %DATABASE% -i "@{fileTitle}"
	sqlcmd -S %SQLSERVER% %CREDENTIALS% -d %DATABASE% -i @{fileTitle}
	
=================================================================================================== */

/* ===================================================================================================
	Name:				@{procName}
	CreatedBy:         	@{authorName}
	EmployeeNumber:    	@{authorCode}
	CreateDate:        	@{date}
	IncidentNumber:    
	Division:           ALL
	Description:	      
	Modified by:       	Rob Blake 

	@{desc}
	
	EXEC @{database}.dbo.@{procName} 'DevUser', 1002, 2016, 1
=================================================================================================== */

USE @{database}

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = '@{procName}')
   DROP PROC @{procName};
GO

CREATE PROCEDURE dbo.@{procName}
(
	@UserName VARCHAR(50),
	@EntityId INT,
	@YearId INT,
	@QuarterId INT
)
AS
BEGIN
	SET NOCOUNT ON;
	
	IF dbo.fUserCanAccessEntity(@UserName, @EntityId) = 0
		RETURN 1;
		
	DECLARE @YearTermId int = (SELECT YearTermId FROM YearTerm WHERE [Year] = @YearId AND TermId = @QuarterId);
	
	??
	
END
