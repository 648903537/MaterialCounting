using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.itac.mes.imsapi.domain.container;
using com.itac.mes.imsapi.client.dotnet;
using com.amtec.forms;
using com.amtec.model;
using com.amtec.action;

namespace com.amtec.action
{
    public class UploadFailureState
    {
        private static IMSApiDotNet imsapi = IMSApiDotNet.loadLibrary();
        private IMSApiSessionContextStruct sessionContext;
        private MainView view;
        private InitModel init;

        public UploadFailureState(IMSApiSessionContextStruct sessionContext, InitModel init, MainView view)
        {
            this.sessionContext = sessionContext;
            this.init = init;
            this.view = view;
        }

        public int UploadFailureAndResultData(string serialNumber, string serialNumberPos,int processLayer,string[] measurevaluelist, string[] failureValueList, int duplicateSerialNumber, float cycleTime, long bookTime, int serialNumberState)
        {
            var measureKeys = new string[] { "ERROR_CODE", "MEASURE_FAIL_CODE", "MEASURE_NAME", "MEASURE_VALUE" };
            var measureValues = measurevaluelist;
            var measureResultValues = new string[] { };
            var failureKeys = new string[] {"COMP_NAME", "ERROR_CODE", "FAILURE_TYPE_CODE" };
            var failureValues = failureValueList;
            var failureResultValues = new string[] { };
            var failureSlipKeys = new string[] { "ERROR_CODE", "TEST_STEP_NAME" };
            var failureSlipValues = new string[] { };
            var failureSlipResultValues = new string[] { };
            LogHelper.Info("begin api trUploadFailureAndResultData (Station:" + init.configHandler.StationNumber + "SN:" + serialNumber + ",SerialNumberPos:" + serialNumberPos + ",process layer:" + processLayer + ")");
            int error = imsapi.trUploadFailureAndResultData(sessionContext, init.configHandler.StationNumber, processLayer, serialNumber, serialNumberPos, serialNumberState, duplicateSerialNumber, cycleTime, bookTime
                , measureKeys, measureValues, out measureResultValues, failureKeys, failureValues, out failureResultValues, failureSlipKeys, failureSlipValues, out failureSlipResultValues);
            string errorMsg = UtilityFunction.GetZHSErrorString(error, init, sessionContext);
            LogHelper.Info("end api trUploadFailureAndResultData (ResultCode = " + error + ")");
            if (error == 0)
            {
                view.errorHandler(0, init.lang.ERROR_API_CALL_ERROR + " trUploadFailureAndResultData " + error, "");
            }
            else
            {
                view.errorHandler(2, init.lang.ERROR_API_CALL_ERROR + " trUploadFailureAndResultData " + error+"("+errorMsg+")", "");
            }
            return error;
        }

        public SerialNumberData[] GetSerialNumberAll(string serialNumber)
        {
            SerialNumberData[] serialNumArray = new SerialNumberData[] { };
            LogHelper.Info("begin trGetSerialNumberBySerialNumberRef(): SN:" + serialNumber);
            int errorSubSN = imsapi.trGetSerialNumberBySerialNumberRef(sessionContext, init.configHandler.StationNumber, serialNumber, "-1", out serialNumArray);
            LogHelper.Info("trGetSerialNumberBySerialNumberRef(): ResultCode :" + errorSubSN + "");
            LogHelper.Info("end trGetSerialNumberBySerialNumberRef():");
            return serialNumArray;
        }
    }
}
