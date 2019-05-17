USE Camris_Archive;
SELECT MAX(dbo.tDSSOutcomeArchived.ReturnDate) AS MaxOfReturnDate
INTO #1
FROM dbo.tDSSOutcomeArchived;
SELECT #1.MaxOfReturnDate, 
       dbo.tDSSOutcomeArchived.ReturnDate, 
       dbo.tDSSOutcomeArchived.DMRNum, 
       dbo.tDSSOutcomeArchived.CurrentRunFlag, 
       dbo.tDSSOutcomeArchived.PreviousRunFlag, 
       dbo.tDSSOutcomeArchived.CL_ID_NUM, 
       dbo.tDSSOutcomeArchived.CL_LAST_NAME, 
       dbo.tDSSOutcomeArchived.CL_FIRST_NAME, 
       dbo.tDSSOutcomeArchived.CL_MIDDLE_INIT, 
       dbo.tDSSOutcomeArchived.CL_SSN_NUM, 
       dbo.tDSSOutcomeArchived.CL_DOB_DT, 
       dbo.tDSSOutcomeArchived.CL_SEX_CD, 
       dbo.tDSSOutcomeArchived.CL_HCB_WVR_TYPE_CD, 
       dbo.tDSSOutcomeArchived.CL_HCM_STS_EFF_DT, 
       dbo.tDSSOutcomeArchived.CL_FOUND_IND, 
       dbo.tDSSOutcomeArchived.CL_MA_ACTIVE_IND, 
       dbo.tDSSOutcomeArchived.CL_SSN_MTCH_IND, 
       dbo.tDSSOutcomeArchived.CL_LAST_NAME_MTCH_IND, 
       dbo.tDSSOutcomeArchived.CL_DOB_MTCH_IND, 
       dbo.tDSSOutcomeArchived.CL_SEX_CD_MTCH_IND, 
       dbo.tDSSOutcomeArchived.AU_NUM_1, 
       dbo.tDSSOutcomeArchived.AU_PROG_CD_1, 
       dbo.tDSSOutcomeArchived.AU_MA_CVRG_GRP_CD_1, 
       dbo.tDSSOutcomeArchived.AU_RR_END_DT_1, 
       dbo.tDSSOutcomeArchived.AU_DO_NUM_1, 
       dbo.tDSSOutcomeArchived.AU_WRKR_NUM_1, 
       dbo.tDSSOutcomeArchived.CL_AU_FIN_RESP_CD_1, 
       dbo.tDSSOutcomeArchived.AU_NUM_2, 
       dbo.tDSSOutcomeArchived.AU_PROG_CD_2, 
       dbo.tDSSOutcomeArchived.AU_MA_CVRG_GRP_CD_2, 
       dbo.tDSSOutcomeArchived.AU_RR_END_DT_2, 
       dbo.tDSSOutcomeArchived.AU_DO_NUM_2, 
       dbo.tDSSOutcomeArchived.AU_WRKR_NUM_2, 
       dbo.tDSSOutcomeArchived.CL_AU_FIN_RESP_CD_2, 
       dbo.tDSSOutcomeArchived.DMR_CL_SSN_NUM, 
       dbo.tDSSOutcomeArchived.DMR_CL_ID_NUM, 
       dbo.tDSSOutcomeArchived.DMR_CL_FIRST_NAME, 
       dbo.tDSSOutcomeArchived.DMR_CL_MIDDLE_INIT, 
       dbo.tDSSOutcomeArchived.DMR_CL_LAST_NAME, 
       dbo.tDSSOutcomeArchived.DMR_CL_DOB_DT, 
       dbo.tDSSOutcomeArchived.DMR_CL_SEX_CD, 
       dbo.tDSSOutcomeArchived.MANAGED_CARE_STATUS, 
       dbo.tDSSOutcomeArchived.EMS_WRKR_LASTNAME, 
       dbo.tDSSOutcomeArchived.EMS_WRKR_FIRSTNAME, 
       dbo.tDSSOutcomeArchived.EMS_PHONE, 
       dbo.tDSSOutcomeArchived.EMS_EXT,
       CASE
           WHEN AU_MA_CVRG_GRP_CD_1 IN('H01', 'L01', 'N01', 'S01', 'S02', 'S03', 'S04', 'S05', 'S95', 'W01')
           THEN AU_MA_CVRG_GRP_CD_1
           WHEN AU_MA_CVRG_GRP_CD_2 IN('H01', 'L01', 'N01', 'S01', 'S02', 'S03', 'S04', 'S05', 'S95', 'W01')
           THEN AU_MA_CVRG_GRP_CD_2
           ELSE ''
       END AS EligibleCoverageGroup,
       CASE
           WHEN AU_MA_CVRG_GRP_CD_1 IN('H01', 'L01', 'N01', 'S01', 'S02', 'S03', 'S04', 'S05', 'S95', 'W01')
           THEN AU_RR_END_DT_1
           WHEN AU_MA_CVRG_GRP_CD_2 IN('H01', 'L01', 'N01', 'S01', 'S02', 'S03', 'S04', 'S05', 'S95', 'W01')
           THEN AU_RR_END_DT_2
           ELSE ''
       END AS EligibleCoverageGroupDate
INTO #2
FROM #1
     INNER JOIN dbo.tDSSOutcomeArchived ON #1.MaxOfReturnDate = dbo.tDSSOutcomeArchived.ReturnDate;
SELECT MAX(dbo.tDSSOutcomeArchived.ReturnDate) AS MaxOfReturnDate1
INTO #3
FROM #1
     RIGHT JOIN dbo.tDSSOutcomeArchived ON #1.MaxOfReturnDate = dbo.tDSSOutcomeArchived.ReturnDate
GROUP BY #1.MaxOfReturnDate
HAVING(((#1.MaxOfReturnDate) IS NULL));
SELECT #3.MaxOfReturnDate1, 
       dbo.tDSSOutcomeArchived.ReturnDate, 
       dbo.tDSSOutcomeArchived.DMRNum, 
       dbo.tDSSOutcomeArchived.CurrentRunFlag, 
       dbo.tDSSOutcomeArchived.PreviousRunFlag, 
       dbo.tDSSOutcomeArchived.CL_ID_NUM, 
       dbo.tDSSOutcomeArchived.CL_LAST_NAME, 
       dbo.tDSSOutcomeArchived.CL_FIRST_NAME, 
       dbo.tDSSOutcomeArchived.CL_MIDDLE_INIT, 
       dbo.tDSSOutcomeArchived.CL_SSN_NUM, 
       dbo.tDSSOutcomeArchived.CL_DOB_DT, 
       dbo.tDSSOutcomeArchived.CL_SEX_CD, 
       dbo.tDSSOutcomeArchived.CL_HCB_WVR_TYPE_CD, 
       dbo.tDSSOutcomeArchived.CL_HCM_STS_EFF_DT, 
       dbo.tDSSOutcomeArchived.CL_FOUND_IND, 
       dbo.tDSSOutcomeArchived.CL_MA_ACTIVE_IND, 
       dbo.tDSSOutcomeArchived.CL_SSN_MTCH_IND, 
       dbo.tDSSOutcomeArchived.CL_LAST_NAME_MTCH_IND, 
       dbo.tDSSOutcomeArchived.CL_DOB_MTCH_IND, 
       dbo.tDSSOutcomeArchived.CL_SEX_CD_MTCH_IND, 
       dbo.tDSSOutcomeArchived.AU_NUM_1, 
       dbo.tDSSOutcomeArchived.AU_PROG_CD_1, 
       dbo.tDSSOutcomeArchived.AU_MA_CVRG_GRP_CD_1, 
       dbo.tDSSOutcomeArchived.AU_RR_END_DT_1, 
       dbo.tDSSOutcomeArchived.AU_DO_NUM_1, 
       dbo.tDSSOutcomeArchived.AU_WRKR_NUM_1, 
       dbo.tDSSOutcomeArchived.CL_AU_FIN_RESP_CD_1, 
       dbo.tDSSOutcomeArchived.AU_NUM_2, 
       dbo.tDSSOutcomeArchived.AU_PROG_CD_2, 
       dbo.tDSSOutcomeArchived.AU_MA_CVRG_GRP_CD_2, 
       dbo.tDSSOutcomeArchived.AU_RR_END_DT_2, 
       dbo.tDSSOutcomeArchived.AU_DO_NUM_2, 
       dbo.tDSSOutcomeArchived.AU_WRKR_NUM_2, 
       dbo.tDSSOutcomeArchived.CL_AU_FIN_RESP_CD_2, 
       dbo.tDSSOutcomeArchived.DMR_CL_SSN_NUM, 
       dbo.tDSSOutcomeArchived.DMR_CL_ID_NUM, 
       dbo.tDSSOutcomeArchived.DMR_CL_FIRST_NAME, 
       dbo.tDSSOutcomeArchived.DMR_CL_MIDDLE_INIT, 
       dbo.tDSSOutcomeArchived.DMR_CL_LAST_NAME, 
       dbo.tDSSOutcomeArchived.DMR_CL_DOB_DT, 
       dbo.tDSSOutcomeArchived.DMR_CL_SEX_CD, 
       dbo.tDSSOutcomeArchived.MANAGED_CARE_STATUS, 
       dbo.tDSSOutcomeArchived.EMS_WRKR_LASTNAME, 
       dbo.tDSSOutcomeArchived.EMS_WRKR_FIRSTNAME, 
       dbo.tDSSOutcomeArchived.EMS_PHONE, 
       dbo.tDSSOutcomeArchived.EMS_EXT,
       CASE
           WHEN AU_MA_CVRG_GRP_CD_1 IN('H01', 'L01', 'N01', 'S01', 'S02', 'S03', 'S04', 'S05', 'S95', 'W01')
           THEN AU_MA_CVRG_GRP_CD_1
           WHEN AU_MA_CVRG_GRP_CD_2 IN('H01', 'L01', 'N01', 'S01', 'S02', 'S03', 'S04', 'S05', 'S95', 'W01')
           THEN AU_MA_CVRG_GRP_CD_2
           ELSE ''
       END AS EligibleCoverageGroup,
       CASE
           WHEN AU_MA_CVRG_GRP_CD_1 IN('H01', 'L01', 'N01', 'S01', 'S02', 'S03', 'S04', 'S05', 'S95', 'W01')
           THEN AU_RR_END_DT_1
           WHEN AU_MA_CVRG_GRP_CD_2 IN('H01', 'L01', 'N01', 'S01', 'S02', 'S03', 'S04', 'S05', 'S95', 'W01')
           THEN AU_RR_END_DT_2
           ELSE ''
       END AS EligibleCoverageGroupDate,
          case when isnull(cast(cast(AU_RR_END_DT_1 as date)as varchar),'') <= getdate() then DATEFROMPARTS(year(getdate()),
                     DATEPART(m, DATEADD(DD,-40,AU_RR_END_DT_1)), 
                     DATEPART(d, DATEADD(DD,-40,AU_RR_END_DT_1)))
                           when isnull(cast(cast(AU_RR_END_DT_1 as date)as varchar),'') >= getdate() then DATEADD(DD,-40,AU_RR_END_DT_1)
                           else ''
                           end as NextRedeterminationDate
INTO #4
FROM #3
     INNER JOIN dbo.tDSSOutcomeArchived ON #3.MaxOfReturnDate1 = dbo.tDSSOutcomeArchived.ReturnDate;
SELECT ddsnum, 
       m.lastname, 
       m.firstname, 
       #2.ReturnDate NewestReturnDate, 
       #4.ReturnDate SecondNewestReturnDate, 
       #2.EligibleCoverageGroup NewestEligibleCoverageGroup, 
       #4.EligibleCoverageGroup SecondNewestEligibleCoverageGroup, 
       #2.EligibleCoverageGroupDate NewestEligibleCoverageGroupDate, 
       #4.EligibleCoverageGroupDate SecondNewestEligibleCoverageGroupDate,
       CASE
           WHEN #2.EligibleCoverageGroup <> #4.EligibleCoverageGroup
           THEN #2.EligibleCoverageGroup
       END AS Coveragegroupchange,
       CASE
           WHEN #2.EligibleCoverageGroupDate <> #4.EligibleCoverageGroupDate
           THEN #2.EligibleCoverageGroupDate
       END AS CoverageGroupChangeDate,
          NextRedeterminationDate,
          
          c.Email CaseManagerEmail
          into #5 
FROM #2
     INNER JOIN #4 ON #2.dmrnum = #4.dmrnum
     INNER JOIN camris..vwExtractClientMain m ON #2.dmrnum = m.ddsnum
       inner join camris..tCTCaseManager c on m.CaseMgr = c.CaseMgrNum

       Select *,      
          case when Coveragegroupchange is null and CoverageGroupChangeDate is not null
           then 
          'Redermination due date has been changed to' +CONVERT(varchar(23), (Convert(Date,CoverageGroupChangeDate)), 121)+'. Please submit the redetermination on '+CONVERT(varchar(23), (Convert(Date,NextRedeterminationDate)), 121)+' for best results'
                 when Coveragegroupchange in ('H01', 'L01', 'N01', 'S01', 'S02', 'S03', 'S04', 'S05', 'S95', 'W01') 
				 then 'Individual has been granted'+ Coveragegroupchange +'(This coverage group covers DDS services)'
                 when Coveragegroupchange is not null and CoverageGroupChangeDate is null 
				 then 'This individual is no longer on Medicaid'
                 when Coveragegroupchange not in ('H01', 'L01', 'N01', 'S01', 'S02', 'S03', 'S04', 'S05', 'S95', 'W01') 
				 then 'Individual has been granted'+ Coveragegroupchange +'(This coverage group covers DDS services)'
				 else ''
			end as Message,
			'Medicaid Coverage Change Notice' as Subject

		from #5 
        where Coveragegroupchange is not null or  CoverageGroupChangeDate is not null

DROP TABLE #1;
DROP TABLE #2;
DROP TABLE #3;
DROP TABLE #4;
DROP TABLE #5;

