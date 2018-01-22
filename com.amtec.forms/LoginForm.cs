using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using com.amtec.configurations;
using com.itac.mes.imsapi.domain.container;
using com.itac.mes.imsapi.client.dotnet;
using com.amtec.forms;
using com.amtec.model;
using com.amtec.action;
using com.amtec.device;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;

namespace com.amtec.forms
{
    public partial class LoginForm : Form
    {
        private IMSApiSessionValidationStruct sessionValidationStruct;
        public IMSApiSessionContextStruct sessionContext = null;
        private SessionContextHeandler sessionContextHandler;
        private static IMSApiDotNet imsapi = IMSApiDotNet.loadLibrary();
        private LanguageResources res;
        public string UserName = "";
        public int LoginResult = 0;
        public bool isCanLogin = false;
        private InitModel initModel;
        private MainView view;
        private ApplicationConfiguration config;

        public LoginForm()
        {
            InitializeComponent();

            this.progressBar1.Value = 0;
            this.progressBar1.Maximum = 100;
            this.progressBar1.Step = 1;

            this.timer1.Interval = 100;
            this.timer1.Tick += new EventHandler(timer_Tick);

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(worker_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!VerifyLoginInfo())
                return;
            LogHelper.Info("Login start...");
            backgroundWorker1.RunWorkerAsync();
            this.lblErrorMsg.Text = "Loading application....";
            this.timer1.Start();
            SetControlStatus(false);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.progressBar1.Value < this.progressBar1.Maximum - 5)
            {
                this.progressBar1.Value++;
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timer1.Stop();
            this.progressBar1.Value = this.progressBar1.Maximum;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //添加你初始化的代码
            res = new LanguageResources();
            CommonModel commonModel = ReadIhasFileData.getInstance();
            if (!isCanLogin)
            {
                sessionContextHandler = new SessionContextHeandler(null, this);
            }
            sessionValidationStruct = new IMSApiSessionValidationStruct();
            sessionValidationStruct.stationNumber = commonModel.Station;
            sessionValidationStruct.stationPassword = "";
            sessionValidationStruct.user = this.txtUserName.Text.Trim();
            sessionValidationStruct.password = this.txtPassword.Text.Trim();
            sessionValidationStruct.client = commonModel.Client;
            sessionValidationStruct.registrationType = commonModel.RegisterType;
            sessionValidationStruct.systemIdentifier = commonModel.Station;
            UserName = this.txtUserName.Text.Trim();

            LoginResult = imsapi.regLogin(sessionValidationStruct, out sessionContext);
            if (LoginResult == 0)
                LogHelper.Info("api regLogin.(error code=" + LoginResult + ")");
            else
                LogHelper.Error("api regLogin.(error code=" + LoginResult + ")");
            LogHelper.Info("Login end...");
            if (LoginResult != IMSApiDotNetConstants.RES_OK)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    SetStatusLabelText("api regLogin error.(error code=" + LoginResult + ")", 1);
                    SetControlStatus(true);
                }));
                return;
            }
            else
            {
                //add by qy 160614
                if (!VerifyUserTeam())
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        SetControlStatus(true);
                    }));
                    return;
                }
                this.Invoke(new MethodInvoker(delegate
                {
                    this.Hide();
                    CloseSP(); //add by qy
                    MainView view = new MainView(this.txtUserName.Text.Trim(), DateTime.Now, sessionContext); //edit by qy
                    view.ShowDialog();
                }));
            }
        }

        public delegate void SetStatusLabelTextDel(string strText, int iCase);
        public void SetStatusLabelText(string strText, int iCase)
        {
            if (this.lblErrorMsg.InvokeRequired)
            {
                SetStatusLabelTextDel setText = new SetStatusLabelTextDel(SetStatusLabelText);
                Invoke(setText, new object[] { strText, iCase });
            }
            else
            {
                this.lblErrorMsg.Text = strText;
                if (iCase == 0)
                {
                    this.lblErrorMsg.ForeColor = Color.Black;
                }
                else if (iCase == 1)
                {
                    this.lblErrorMsg.ForeColor = Color.Red;
                }
            }
        }

        private void SetControlStatus(bool isOK)
        {
            this.btnOK.Enabled = isOK;
            this.txtPassword.Enabled = isOK;
            this.txtUserName.Enabled = isOK;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(null, null);
            }
        }

        private bool VerifyLoginInfo()
        {
            bool isValidate = true;
            if (string.IsNullOrEmpty(this.txtUserName.Text.Trim()) || string.IsNullOrEmpty(this.txtPassword.Text.Trim()))
            {
                SetStatusLabelText("Pls input user name/password.", 1);
                isValidate = false;
            }
            return isValidate;
        }

        #region add by qy
        private void LoginForm_Load(object sender, EventArgs e)
        {
            config = new ApplicationConfiguration(sessionContext, view);
            OpenLogInCOM();
            InitLogInType();
        }

        private void OpenLogInCOM()
        {
            initModel = new InitModel();
            try
            {
                initModel.configHandler = config;
            }
            catch
            {
                LogHelper.Error("Config error.");
            }

            try
            {
                if (config.LogInType == "COM")
                {
                    initModel.scannerHandler = new ScannerHeandler(initModel, view);
                    initModel.scannerHandler.handler().DataReceived += new SerialDataReceivedEventHandler(LogInDataRecivedHeandler);
                    initModel.scannerHandler.handler().Open();
                }
            }
            catch
            {
                SetStatusLabelText("COM打开异常", 1);
                LogHelper.Error("COM打开异常");
            }
        }

        public void LogInDataRecivedHeandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            try
            {
                Thread.Sleep(200);
                Byte[] bt = new Byte[sp.BytesToRead];
                sp.Read(bt, 0, sp.BytesToRead);
                string indata = System.Text.Encoding.ASCII.GetString(bt).Trim();
                //indata = sp.ReadLine();
                LogHelper.Info("Scan LogIn(original): " + indata);
                Match match = Regex.Match(indata, config.LogInRegular);
                if (match.Success)
                {
                    GroupCollection coSub = match.Groups;
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.txtUserName.Text = coSub[1].ToString();
                        this.txtPassword.Text = coSub[2].ToString();
                        btnOK_Click(null, null);

                    }));
                    return;
                }

                SetStatusLabelText("错误条码", 1);
                LogHelper.Error("错误条码");
            }
            catch (Exception ex)
            {
                SetStatusLabelText(ex.Message, 1);
                LogHelper.Error(ex.Message);
            }
            finally
            {
                initModel.scannerHandler.handler().DiscardInBuffer();
            }
        }

        private void CloseSP()
        {
            if (config.LogInType == "COM")
            {
                if (initModel.scannerHandler.handler().IsOpen)
                {
                    initModel.scannerHandler.handler().Close();
                }
            }
        }
        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.txtPassword.Text.Trim()))
                {
                    this.txtPassword.Focus();
                }
                else
                {
                    btnOK_Click(null, null);
                }
            }
        }

        private void InitLogInType()
        {
            if (config.LogInType == "COM")
            {
                this.txtUserName.ReadOnly = true;
                this.txtPassword.ReadOnly = true;
            }
        }
        #endregion
        private bool VerifyUserTeam()
        {
            bool isValid = true;
            if (config.UserTeam != "" && config.UserTeam != null)
            {
                GetUserData getUser = new GetUserData(sessionContext);
                string[] mdataGetUserDataValues = getUser.mdataGetUserData(this.txtUserName.Text.Trim(), this.txtPassword.Text.Trim(), config.StationNumber);
                if (mdataGetUserDataValues != null && mdataGetUserDataValues.Length > 0)
                {
                    string teamnumber = mdataGetUserDataValues[2];
                    if (!config.UserTeam.Contains(teamnumber))
                    {
                        SetStatusLabelText("User Team not authorized", 1);
                        isValid = false;
                    }
                }
                else
                {
                    SetStatusLabelText("User Team not authorized", 1);
                    isValid = false;
                }
            }
            return isValid;
        }
    }
}
