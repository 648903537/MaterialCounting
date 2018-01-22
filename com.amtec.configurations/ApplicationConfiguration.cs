using com.amtec.action;
using com.amtec.forms;
using com.amtec.model;
using com.itac.mes.imsapi.domain.container;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace com.amtec.configurations
{
    public class ApplicationConfiguration
    {
        public String StationNumber { get; set; }

        public String Client { get; set; }

        public String RegistrationType { get; set; }

        public String SerialPort { get; set; }

        public String BaudRate { get; set; }

        public String Parity { get; set; }

        public String StopBits { get; set; }

        public String DataBits { get; set; }

        public String NewLineSymbol { get; set; }

        public String DLExtractPattern { get; set; }

        public String MBNExtractPattern { get; set; }

        public String MDAPath { get; set; }

        public String EquipmentExtractPattern { get; set; }

        public String OpacityValue { get; set; }

        public String LocationXY { get; set; }

        public String IPAddress { get; set; }

        public String Port { get; set; }

        public String OutputEnter { get; set; }

        #region add by qy
        public String LogFileFolder { get; set; }

        public String LogTransOK { get; set; }

        public String LogTransError { get; set; }

        public String ChangeFileName { get; set; }

        public String LineRegular { get; set; }

        public String ResultData { get; set; }

        public String SNICT { get; set; }

        public String SNState { get; set; }

        public String LowerLimit { get; set; }

        public String UpperLimit { get; set; }

        public String MeasureFailCode { get; set; }

        public String MeasureName { get; set; }

        public String MeasureValue { get; set; }

        public String UNIT { get; set; }

        public String MNRegular { get; set; }

        public String kValue { get; set; }

        public String MEGValue { get; set; }

        public String MValue { get; set; }

        public String NValue { get; set; }

        public String UValue { get; set; }

        public String PValue { get; set; }

        public String Language { get; set; }

        public String FileType { get; set; }

        public String IsListenerFolder { get; set; }

        public String OutSerialPort { get; set; }

        public String OutBaudRate { get; set; }

        public String OutParity { get; set; }

        public String OutStopBits { get; set; }

        public String OutDataBits { get; set; }

        public String DataOutputInterface { get; set; }

        public String TrayExtractPattern { get; set; }

        public String LogInRegular { get; set; }

        public String LogInType { get; set; }

        public String CheckListFolder { get; set; }

        public String IsSelectWO { get; set; }

        public String IsCheckList { get; set; }

        public String IsMaterialSetup { get; set; }

        public String IsEquipSetup { get; set; }

        public String YSLine{get; set;}

        public String YSPos { get; set; }

        public String ASCLine { get; set; }

        public String ASCPos { get; set; }

        public String IQTYPattern { get; set; }

        public String SelectMode { get; set; }

        public String DataSleepTime { get; set; }

        public String FileNamePattern { get; set; }

        public String FilterByFileName { get; set; }

        public String UserTeam { get; set; }

        public String PORTAL_SERVER_CONNECT { get; set; }
        #endregion

        Dictionary<string, string> dicConfig = null;
        public ApplicationConfiguration(IMSApiSessionContextStruct sessionContext, MainView view)
        {

            try
            {
                CommonModel commonModel = ReadIhasFileData.getInstance();
                StationNumber = commonModel.Station;
                Client = commonModel.Client;
                RegistrationType = commonModel.RegisterType;
                if(commonModel.UpdateConfig=="L")
                {
                    XDocument config = XDocument.Load("ApplicationConfig.xml");
                    SerialPort = GetDescendants("SerialPort", config);//config.Descendants("SerialPort").First().Value;
                    BaudRate = GetDescendants("BaudRate", config);//config.Descendants("BaudRate").First().Value;
                    Parity = GetDescendants("Parity", config);//config.Descendants("Parity").First().Value;
                    StopBits = GetDescendants("StopBits", config);//config.Descendants("StopBits").First().Value;
                    DataBits = GetDescendants("DataBits", config);//config.Descendants("DataBits").First().Value;
                    NewLineSymbol = GetDescendants("NewLineSymbol", config);//config.Descendants("NewLineSymbol").First().Value;
                    DLExtractPattern = GetDescendants("DLExtractPattern", config);//config.Descendants("DLExtractPattern").First().Value;
                    MBNExtractPattern = GetDescendants("MBNExtractPattern", config);//config.Descendants("MBNExtractPattern").First().Value;
                    EquipmentExtractPattern = GetDescendants("EquipmentExtractPattern", config);//config.Descendants("EquipmentExtractPattern").First().Value;
                    OpacityValue = GetDescendants("OpacityValue", config);//config.Descendants("OpacityValue").First().Value;
                    LocationXY = GetDescendants("LocationXY", config);//config.Descendants("LocationXY").First().Value;
                    MDAPath = GetDescendants("MDAPath", config);//config.Descendants("MDAPath").First().Value;
                    OutputEnter = GetDescendants("OutputEnter", config);//config.Descendants("OutputEnter").First().Value;

                    #region add by qy
                    Language = GetDescendants("Language", config);//config.Descendants("Language").First().Value;
                    OutSerialPort = GetDescendants("OutSerialPort", config);//config.Descendants("OutSerialPort").First().Value;
                    OutBaudRate = GetDescendants("OutBaudRate", config);//config.Descendants("OutBaudRate").First().Value;
                    OutParity = GetDescendants("OutParity", config);//config.Descendants("OutParity").First().Value;
                    OutStopBits = GetDescendants("OutStopBits", config);//config.Descendants("OutStopBits").First().Value;
                    OutDataBits = GetDescendants("OutDataBits", config);//config.Descendants("OutDataBits").First().Value;
                    DataOutputInterface = GetDescendants("DataOutputInterface", config);//config.Descendants("DataOutputInterface").First().Value;
                    TrayExtractPattern = GetDescendants("TrayExtractPattern", config);//config.Descendants("TrayExtractPattern").First().Value;
                    LogInRegular = GetDescendants("LogInRegular", config);//config.Descendants("LogInRegular").First().Value;
                    LogInType = GetDescendants("LogInType", config);//config.Descendants("LogInType").First().Value;
                    CheckListFolder = GetDescendants("CheckListFolder", config);//config.Descendants("CheckListFolder").First().Value;
                    IsSelectWO = GetDescendants("IsSelectWO", config);//config.Descendants("IsSelectWO").First().Value;
                    IsCheckList = GetDescendants("IsCheckList", config);//config.Descendants("IsCheckList").First().Value;
                    IsMaterialSetup = GetDescendants("IsMaterialSetup", config);//config.Descendants("IsMaterialSetup").First().Value;
                    IsEquipSetup = GetDescendants("IsEquipSetup", config);//config.Descendants("IsEquipSetup").First().Value;
                    YSLine = GetDescendants("YSLine", config);//config.Descendants("YSLine").First().Value;
                    YSPos = GetDescendants("YSPos", config);//config.Descendants("YSPos").First().Value;
                    ASCLine = GetDescendants("ASCLine", config);//config.Descendants("ASCLine").First().Value;
                    ASCPos = GetDescendants("ASCPos", config);//config.Descendants("ASCPos").First().Value;
                    IQTYPattern = GetDescendants("IQTYPattern", config);//config.Descendants("IQTYPattern").First().Value;
                    SelectMode = GetDescendants("SelectMode", config);//config.Descendants("SelectMode").First().Value;
                    DataSleepTime = GetDescendants("DataSleepTime", config);//config.Descendants("DataSleepTime").First().Value;
                    FilterByFileName = GetDescendants("FilterByFileName", config);//config.Descendants("FilterByFileName").First().Value;
                    FileNamePattern = GetDescendants("FileNamePattern", config);//config.Descendants("FileNamePattern").First().Value;
                    UserTeam = GetDescendants("AUTH_TEAM", config);//config.Descendants("UserTeam").First().Value;
                    PORTAL_SERVER_CONNECT = GetDescendants("PORTAL_SERVER_CONNECT", config);
                    #endregion
                }
                else
                {
                    dicConfig = new Dictionary<string, string>();
                    ConfigManage configHandler = new ConfigManage(sessionContext, view);
                    if (commonModel.UpdateConfig == "Y")
                    {
                        //int error = configHandler.DeleteConfigParameters(commonModel.APPTYPE);
                        //if (error == 0 || error == -3303 || error == -3302)
                        //{
                        //    WriteParameterToiTac(configHandler);
                        //}
                        string[] parametersValue = configHandler.GetParametersForScope(commonModel.APPTYPE);
                        if (parametersValue != null && parametersValue.Length > 0)
                        {
                            foreach (var parameterID in parametersValue)
                            {
                                configHandler.DeleteConfigParametersExt(parameterID);
                            }
                        }
                        WriteParameterToiTac(configHandler);
                    }
                    List<ConfigEntity> getvalues = configHandler.GetConfigData(commonModel.APPID, commonModel.APPTYPE, commonModel.Cluster, commonModel.Station);
                    if (getvalues != null)
                    {
                        foreach (var item in getvalues)
                        {
                            if (item != null)
                            {
                                string[] strs = item.PARAMETER_NAME.Split(new char[] { '.' });
                                dicConfig.Add(strs[strs.Length - 1], item.CONFIG_VALUE);
                            }
                        }
                    }
                    SerialPort = GetParameterValue("SerialPort");
                    BaudRate = GetParameterValue("BaudRate");
                    Parity = GetParameterValue("Parity");
                    StopBits = GetParameterValue("StopBits");
                    DataBits = GetParameterValue("DataBits");
                    NewLineSymbol = GetParameterValue("NewLineSymbol");
                    DLExtractPattern = GetParameterValue("DLExtractPattern");
                    MBNExtractPattern = GetParameterValue("MBNExtractPattern");
                    EquipmentExtractPattern = GetParameterValue("EquipmentExtractPattern");
                    OpacityValue = GetParameterValue("OpacityValue");
                    LocationXY = GetParameterValue("LocationXY");
                    MDAPath = GetParameterValue("MDAPath");
                    OutputEnter = GetParameterValue("OutputEnter");

                    #region add by qy
                    Language = GetParameterValue("Language");
                    OutSerialPort = GetParameterValue("OutSerialPort");
                    OutBaudRate = GetParameterValue("OutBaudRate");
                    OutParity = GetParameterValue("OutParity");
                    OutStopBits = GetParameterValue("OutStopBits");
                    OutDataBits = GetParameterValue("OutDataBits");
                    DataOutputInterface = GetParameterValue("DataOutputInterface");
                    TrayExtractPattern = GetParameterValue("TrayExtractPattern");
                    LogInRegular = GetParameterValue("LogInRegular");
                    LogInType = GetParameterValue("LogInType");
                    CheckListFolder = GetParameterValue("CheckListFolder");
                    IsSelectWO = GetParameterValue("IsSelectWO");
                    IsCheckList = GetParameterValue("IsCheckList");
                    IsMaterialSetup = GetParameterValue("IsMaterialSetup");
                    IsEquipSetup = GetParameterValue("IsEquipSetup");
                    YSLine = GetParameterValue("YSLine");
                    YSPos = GetParameterValue("YSPos");
                    ASCLine = GetParameterValue("ASCLine");
                    ASCPos = GetParameterValue("ASCPos");
                    IQTYPattern = GetParameterValue("IQTYPattern");
                    SelectMode = GetParameterValue("SelectMode");
                    DataSleepTime = GetParameterValue("DataSleepTime");
                    FilterByFileName = GetParameterValue("FilterByFileName");
                    FileNamePattern = GetParameterValue("FileNamePattern");
                    #endregion
                }
                
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        private string GetParameterValue(string parameterName)
        {
            if (dicConfig.ContainsKey(parameterName))
            {
                return dicConfig[parameterName];
            }
            else
            {
                return "";
            }
        }

        private void WriteParameterToiTac(ConfigManage configHandler)
        {
            GetApplicationDatas getData = new GetApplicationDatas();
            List<ParameterEntity> entityList = getData.GetApplicationEntity();
            string[] strs = GetParameterString(entityList);
            string[] strvalues = GetValueString(entityList);
            if (strs != null && strs.Length > 0)
            {
                int errorCode = configHandler.CreateConfigParameter(strs);
                if (errorCode == 0 || errorCode == 5)
                {
                    CommonModel commonModel = ReadIhasFileData.getInstance();
                    int re = configHandler.UpdateParameterValues(commonModel.APPID, commonModel.APPTYPE, commonModel.Cluster, commonModel.Station, strvalues);
                }
            }

            //if (entityList.Count > 0)
            //{
            //    List<ParameterEntity> entitySubList = null;
            //    CommonModel commonModel = ReadIhasFileData.getInstance();
            //    foreach (var entity in entityList)
            //    {
            //        entitySubList = new List<ParameterEntity>();
            //        entitySubList.Add(entity);
            //        string[] strs = GetParameterString(entitySubList);
            //        string[] strvalues = GetValueString(entitySubList);
            //        if (strs != null && strs.Length > 0)
            //        {
            //            int errorCode = configHandler.CreateConfigParameter(strs);
            //            if (errorCode == 0 || errorCode == 5)
            //            {                           
            //                int re = configHandler.UpdateParameterValues(commonModel.APPID, commonModel.APPTYPE, commonModel.Cluster, commonModel.Station, strvalues);
            //            }
            //            else if (errorCode == -3301)//Parameter already exists
            //            {
            //                int re = configHandler.UpdateParameterValues(commonModel.APPID, commonModel.APPTYPE, commonModel.Cluster, commonModel.Station, strvalues);
            //            }
            //        }
            //    }
            //}
        }

        private string[] GetParameterString(List<ParameterEntity> entityList)
        {
            List<string> strList = new List<string>();
            foreach (var entity in entityList)
            {
                strList.Add(entity.PARAMETER_DESCRIPTION);
                strList.Add(entity.PARAMETER_DIMPATH);
                strList.Add(entity.PARAMETER_DISPLAYNAME);
                strList.Add(entity.PARAMETER_NAME);
                strList.Add(entity.PARAMETER_PARENT_NAME);
                strList.Add(entity.PARAMETER_SCOPE);
                strList.Add(entity.PARAMETER_TYPE_NAME);
            }
            return strList.ToArray();
        }

        private string[] GetValueString(List<ParameterEntity> entityList)
        {
            List<string> strList = new List<string>();
            foreach (var entity in entityList)
            {
                if (entity.PARAMETER_VALUE == "")
                    continue;
                strList.Add(entity.PARAMETER_VALUE);
                strList.Add(entity.PARAMETER_NAME);

            }
            return strList.ToArray();
        }

        private string GetDescendants(string parameter, XDocument _config)
        {
            try
            {
                string value = _config.Descendants(parameter).First().Value;

                return value;
            }
            catch
            {
                LogHelper.Error("Parameter is not exist." + parameter);
                return "";
            }
        }
    }
}
