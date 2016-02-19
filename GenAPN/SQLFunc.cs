using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GenAPN
{
    class SQLFunc
    {
        private static string strConn;
        private static SqlDataAdapter SDA;
        private static DataSet DS;

        //private string strOriSN;
        //private string strSN;


        public SQLFunc()
        {
            //strConn = GenAPN.Properties.Resources.ResourceManager.GetString("ConnStr");
            //strConn = GenAPN.Properties.Settings.Default.ServerConnStr;

            
        }

        /// <summary>
        /// Get the original Serial Number for DCP units
        /// </summary>
        /// <param name="tmpSN"></param>
        /// <returns></returns>
        private string GetOriginalSN(string tmpSN)
        {
            string OriginalSN = "";
            using (SqlConnection cn = new SqlConnection(strConn))
            {
                cn.Open();

                using (SqlCommand QryCmd = cn.CreateCommand())
                {
                    QryCmd.CommandText = " SELECT QTA_SN " +
                                           " FROM datRMAEquipments " +
                                           " WHERE NEWQTA_SN= '" + tmpSN + "' ";

                    SDA = new SqlDataAdapter(QryCmd.CommandText, cn);
                    DS = new DataSet();

                    SDA.Fill(DS, "GetOriginalSN");

                    if (DS.Tables[""].Rows.Count != 0)
                    {
                        OriginalSN = DS.Tables[""].Rows[0].ItemArray[0].ToString();
                        return OriginalSN;
                    }
                    else
                    {
                        return OriginalSN;
                    }
                }
            }
        }



        /// <summary>
        /// Get LCD component P/N & specification
        /// </summary>
        /// <param name="tmpSN"></param>
        /// <returns></returns>
        private string GetLCD(string tmpSN)
        {
            string compSpec = "";
            using (SqlConnection cn = new SqlConnection(strConn))
            {
                cn.Open();
                using (SqlCommand QryCmd = cn.CreateCommand())
                {
                    QryCmd.CommandText = " select top 1 CompSpec=(select Top 1 CompSpec from CompSpec WHERE CompName='LCD'and (ApplePN=A.Comp_PN OR QuantaPN = A.Comp_PN)) " +
                                         " from CompSN_Fetch_Check A " +
                                         " where PAL_SN = '" + tmpSN + "' " +
                                         " and (Comp_PN like 'AA%' or Comp_PN like '646%') ";

                    SDA = new SqlDataAdapter(QryCmd.CommandText, cn);
                    DS = new DataSet();

                    SDA.Fill(DS, "GetLCD");

                    if (DS.Tables["GetLCD"].Rows.Count == 1)
                    {
                        compSpec = DS.Tables["GetLCD"].Rows[0].ItemArray[0].ToString();
                        //compSpec = DS.Tables["GetLCD"].Rows[0].ItemArray[2].ToString();
                        return compSpec;
                    }
                    else
                    {
                        return compSpec;
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tmpSN"></param>
        /// <returns></returns>
        private string GetBoard(string tmpSN)
        {
            string compSpec = "";
            using (SqlConnection cn = new SqlConnection(strConn))
            {
                cn.Open();
                using (SqlCommand QryCmd = cn.CreateCommand())
                {
                    QryCmd.CommandText = " select TOP 1 CompSpec = (select top 1 CompSpec from CompSpec where CompName = 'Board' and (ApplePN=A.Comp_PN OR QuantaPN = A.Comp_PN ) ),Comp_SN  " +
                                         " from CompSN_Fetch_Check A " +
                                         " where PAL_SN = '" + tmpSN + "' " +
                                         " and ((Comp_PN like '31PI%' or  Comp_PN like '21PI%') or Comp_PN like '639%' ) ";

                    SDA = new SqlDataAdapter(QryCmd.CommandText, cn);
                    DS = new DataSet();

                    SDA.Fill(DS, "GetBoard");

                    if (DS.Tables["GetBoard"].Rows.Count == 1)
                    {
                        compSpec = DS.Tables["GetBoard"].Rows[0].ItemArray[0].ToString();
                        //compSpec = DS.Tables["GetLCD"].Rows[0].ItemArray[2].ToString();
                        return compSpec;
                    }
                    else
                    {
                        return compSpec;
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tmpSN"></param>
        /// <returns></returns>
        private string GetRAM(string tmpSN)
        {
            string compSpec = "";
            using (SqlConnection cn = new SqlConnection(strConn))
            {
                cn.Open();
                using (SqlCommand QryCmd = cn.CreateCommand())
                {
                    QryCmd.CommandText = " select TOP 1 CompSpec = (select top 1 CompSpec from CompSpec where CompName = 'RAM' and (ApplePN=A.Comp_PN OR QuantaPN = A.Comp_PN)  ) " +
                                         " from CompSN_Fetch_Check A " +
                                         " where PAL_SN = '" + tmpSN + "' " +
                                         " and (Comp_PN like 'ATR%' or Comp_PN like '333%') ";

                    SDA = new SqlDataAdapter(QryCmd.CommandText, cn);
                    DS = new DataSet();

                    SDA.Fill(DS, "GetRAM");

                    if (DS.Tables["GetRAM"].Rows.Count == 1)
                    {
                        compSpec = DS.Tables["GetRAM"].Rows[0].ItemArray[0].ToString();
                        //compSpec = DS.Tables["GetLCD"].Rows[0].ItemArray[2].ToString();
                        return compSpec;
                    }
                    else
                    {
                        return compSpec;
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="TmpSN"></param>
        /// <returns></returns>
        private int GetRAMQTY(string TmpSN)
        {
            int RAM_Qty = 0;
            using (SqlConnection cn = new SqlConnection(strConn))
            {
                cn.Open();
                using (SqlCommand QryCmd = new SqlCommand())
                {
                    QryCmd.CommandText = " select ROW = COUNT(Comp_PN) " +
                                         " from CompSN_Fetch_Check A " +
                                         " where PAL_SN = '" + TmpSN + "' " +
                                         " AND (Comp_PN like 'ATR%' or Comp_PN like '333%') ";

                    SDA = new SqlDataAdapter(QryCmd.CommandText, cn);
                    DS = new DataSet();

                    SDA.Fill(DS, "GetRAMQTY");

                    if (DS.Tables["GetRAMQTY"].Rows.Count == 1)
                    {
                        RAM_Qty = Int32.Parse(DS.Tables["GetRAMQTY"].Rows[0].ItemArray[0].ToString());
                        return RAM_Qty;
                    }
                    else
                    {
                        return RAM_Qty;
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tmpSN"></param>
        /// <returns></returns>
        private string GetRAMMLB(string tmpSN)
        {
            string compSpec = "";
            using (SqlConnection cn = new SqlConnection(strConn))
            {
                cn.Open();
                using (SqlCommand QryCmd = cn.CreateCommand())
                {
                    QryCmd.CommandText = " select TOP 1 CompSpec = (select top 1 CompSpec from CompSpec where CompName = 'RAM' and (ApplePN = A.Comp_PN OR QuantaPN = A.Comp_PN) ), PAL_SN " +
                                         " from CompSN_Fetch_Check A " +
                                         " where PAL_SN = '" + tmpSN + "' " +
                                         " and (Comp_PN like '21P%' or Comp_PN like '31P%' or Comp_PN like '639%') ";

                    SDA = new SqlDataAdapter(QryCmd.CommandText, cn);
                    DS = new DataSet();

                    SDA.Fill(DS, "GetRAMMLB");

                    if (DS.Tables["GetRAMMLB"].Rows.Count >= 1)
                    {
                        compSpec = DS.Tables["GetRAMMLB"].Rows[0].ItemArray[0].ToString();
                        //compSpec = DS.Tables["GetLCD"].Rows[0].ItemArray[2].ToString();
                        return compSpec;
                    }
                    else
                    {
                        return compSpec;
                    }
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="tmpSN"></param>
        /// <returns></returns>
        private string GetVRAM(string tmpSN)
        {
            string compSpec = "";
            using (SqlConnection cn = new SqlConnection(strConn))
            {
                cn.Open();
                using (SqlCommand QryCmd = cn.CreateCommand())
                {
                    QryCmd.CommandText = " select TOP 1 CompSpec = (select top 1 CompSpec from CompSpec where CompName = 'VRAM' and (ApplePN=A.Comp_PN OR QuantaPN = A.Comp_PN ))  " +
                                         " from CompSN_Fetch_Check A " +
                                         " where PAL_SN = '" + tmpSN + "' " +
                                         " and (Comp_PN like '31PI%' or  Comp_PN like '21PI%' or Comp_PN like '639%' ) ";

                    SDA = new SqlDataAdapter(QryCmd.CommandText, cn);
                    DS = new DataSet();

                    SDA.Fill(DS, "GetRAMMLB");

                    if (DS.Tables["GetRAMMLB"].Rows.Count >= 1)
                    {
                        compSpec = DS.Tables["GetRAMMLB"].Rows[0].ItemArray[0].ToString();
                        //compSpec = DS.Tables["GetLCD"].Rows[0].ItemArray[2].ToString();
                        return compSpec;
                    }
                    else
                    {
                        return compSpec;
                    }
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="tmpSN"></param>
        /// <returns></returns>
        private string GetHDD(string tmpSN)
        {
            string compSpec = "";
            using (SqlConnection cn = new SqlConnection(strConn))
            {
                cn.Open();
                using (SqlCommand QryCmd = cn.CreateCommand())
                {
                    QryCmd.CommandText = " select CompSpec = (select top 1 CompSpec from CompSpec where CompName = 'HDD'and(ApplePN=A.Comp_PN OR QuantaPN = A.Comp_PN )),PAL_SN " +
                                         " from CompSN_Fetch_Check A " +
                                         " where PAL_SN = '" + tmpSN + "' " +
                                         " and (Comp_PN like 'AB%' or Comp_PN like '655%') " +
                                         " order by Comp_PN ";

                    SDA = new SqlDataAdapter(QryCmd.CommandText, cn);
                    DS = new DataSet();

                    SDA.Fill(DS, "GetHDD");

                    if (DS.Tables["GetHDD"].Rows.Count > 1)
                    {
                        compSpec = DS.Tables["GetHDD"].Rows[0].ItemArray[0].ToString();
                        compSpec += DS.Tables["GetHDD"].Rows[1].ItemArray[0].ToString();
                        return compSpec;
                    }
                    if (DS.Tables["GetHDD"].Rows.Count == 1)
                    {
                        compSpec = DS.Tables["GetHDD"].Rows[0].ItemArray[0].ToString();
                        return compSpec;
                    }
                    else
                    {
                        return compSpec;
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tmpSN"></param>
        /// <returns></returns>
        public string GetRegion(string tmpSN)
        {
            string strRegion = "";
            using (SqlConnection cn = new SqlConnection(strConn))
            {
                cn.Open();
                using (SqlCommand QryCmd = cn.CreateCommand())
                {
                    QryCmd.CommandText = " select case " +
                                           " when Region = 'B' then 'GBR' " +
                                           " when Region = 'D' then 'DEU' " +
                                           " when Region = 'F' then 'FRA' " +
                                           " when Region = 'T' then 'ITA' " +
                                           " when Region = 'Y' then 'ESP' " +
                                           " when Region = 'J' then 'JPN' " +
                                           " when Region = 'X' then 'AUS' " +
                                           " when Region = 'FN' then 'BEL' " +
                                           " when Region = 'SM' then 'SWM' " +
                                           " when Region = 'LL' then 'USA' " +
                                           " when Region = 'ZP' then 'ITP' " +

                                           " end " +
                                           " from datRMAEquipmentUnits_precheck" +
                                           " where QTA_SN = '" + tmpSN + "' ";

                    SDA = new SqlDataAdapter(QryCmd.CommandText, cn);
                    DS = new DataSet();

                    SDA.Fill(DS, "GetRegion");

                    if (DS.Tables["GetRegion"].Rows.Count == 1)
                    {
                        strRegion = DS.Tables["GetRegion"].Rows[0].ItemArray[0].ToString();
                        return strRegion;
                    }
                    else
                    {
                        return strRegion;
                    }
                }
            }
        }



    }
}
