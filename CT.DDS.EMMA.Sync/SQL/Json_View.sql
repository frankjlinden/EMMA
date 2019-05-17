create view vwMedicaidCoverageChange as select 
(Select
ddsnum
,lastname
,firstname
,NewestReturnDate
,SecondNewestReturnDate
,NewestEligibleCoverageGroup
,SecondNewestEligibleCoverageGroup
,Coveragegroupchange
,CoverageGroupChangeDate
,NextRedeterminationDate
,CaseManagerEmail
,[Message]
From EMMA

 For JSON Auto
)
AS X