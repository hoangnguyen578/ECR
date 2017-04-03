using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECRCommXLib;
namespace CShapECRTester
{
    public partial class Form1 : Form
    {
        
        //PaymentRequestData pprd;
        //ECRLibcls pLib;
        //event EventHandler<ECRCommXLib.HEventArgs> CallBack;
        //public event EventHandler<ECRCommXLib.HEventArgs> SampleEvent;
        ECRLibcls pLib;
        int timeout = 90;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //pLib = new ECRLibcls();
            comboBox1.SelectedIndex = 0;
            cboComType.SelectedIndex = 0;
            //pLib.setSocketComm("192.168.2.1",4779);
            //pLib.setUserTimeOut(timeout);
            if (pLib == null)
            {
                pLib = new ECRLibcls(ECRLibcls.eComType.ETH);
                //pLib.setSocketComm("192.168.2.2", 1250);
                pLib.setSocketComm(this.TCPIP.Text, Convert.ToInt32(POSPORT.Text));
                //pLib.setSocketComm("127.0.0.1", 1250);
                pLib.setUserTimeOut(timeout * 1000);
            }
            //if (pLib == null)
            //{
            //    pLib = new ECRLibcls(ECRLibcls.eComType.COM);
            //    pLib.setPortComm("COM2", "9600");
            //    pLib.setUserTimeOut(timeout * 1000);
            //}
            button6_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (pLib == null)
                {
                    pLib = new ECRLibcls(ECRLibcls.eComType.ETH);
                    pLib.setSocketComm(this.TCPIP.Text, Convert.ToInt32(POSPORT.Text));
                    pLib.setUserTimeOut(30 * 1000);
                }
                int amount = Convert.ToInt32(this.Amount.Text);
                int add_amount = Convert.ToInt32(this.AddAmount.Text);
                int bindex=0;
                if (comboBox1.SelectedIndex == 0)
                {
                    bindex = 704;
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    bindex = 840;
                }
                
                    
                if (pLib.PosPurchaseMer(amount, add_amount, bindex, "HOST TEXT"))
                {
                    this.PAN.Text = pLib.GetPAN();
                    this.HolderName.Text = pLib.getHolderName();
                    //this.Amount.Text = (pLib.getTotalAmount()).ToString();
                    //this.AddAmount.Text = (pLib.getAddAmount()).ToString();
                    this.TerminalID.Text = pLib.getTerminalId();
                    this.MerchantID.Text = pLib.getMerchantId();
                    this.BatchNumber.Text = pLib.getBatchNum().ToString();
                    this.IssuerName.Text = pLib.getIssName();
                    this.RRN.Text = pLib.GetRRN();
                    this.Invoice.Text = pLib.getInvoiceNum();
                    this.AuthCode.Text = pLib.getAppCode();
                    this.ResponseCode.Text = pLib.getRspCode();
                }
                else
                {
                    MessageBox.Show(pLib._Result.Message);
                }
                if (pLib._Result.Code != 0)
                {
                    MessageBox.Show(String.Concat("Not success, ","Code: ", pLib._Result.Code.ToString(), " Message: ", pLib._Result.Message));
                    return;
                }
                if (checkBox1.Checked)
                {
                    button1_Click(sender, e);
                    return;
                }

                //MessageBox.Show(String.Concat("Code: ", pLib._Result.Code.ToString(), " Message: ", pLib._Result.Message));
                if (MessageBox.Show("Confirm?", "Title", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    button1_Click(sender, e);
                else
                    button4_Click_2(sender, e);

            }
            catch (ECRCommXLib.TransportException te)
            {
                MessageBox.Show(te.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int v = 555500;
            //byte[] storage = new byte[4];
            //storage[0] = (byte)(v >> 24);
            //storage[1] = (byte)(v >> 16);
            //storage[2] = (byte)(v >> 8);
            //storage[3] = (byte)v;
            //String s = "This is my Visual Basic 6.0";
            //Byte[] storage = System.Text.Encoding.ASCII.GetBytes(s);
            //s = "155550000a1";
            ////storage = System.Text.Encoding.ASCII.GetBytes("155550000a1");
            //return;
            if (pLib != null)
            {
                pLib.CompletionAcknowledge();
                //MessageBox.Show("Confirm OK");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //ECRlibcls.PosXBalance();
            //RouterAnswer(ECRlibcls);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int inv = 0;
                if (this.VoidNumber.Text.Length > 0)
                    inv = Convert.ToInt32(this.VoidNumber.Text);
                if (pLib == null)
                {
                    pLib = new ECRLibcls(ECRLibcls.eComType.ETH);
                    pLib.setSocketComm(this.TCPIP.Text, Convert.ToInt32(POSPORT.Text));
                    pLib.setUserTimeOut(timeout * 1000);
                }

                if (pLib.PosVoid(inv))
                {
                    this.RRN.Text = pLib.GetRRN();
                    this.AuthCode.Text = pLib.getAppCode();
                    this.ResponseCode.Text = pLib.getRspCode();
                }
                else
                {
                    MessageBox.Show(pLib._Result.Message);
                }
                if (pLib._Result.Code != 0)
                {
                    MessageBox.Show(String.Concat("Not success, ", "Code: ", pLib._Result.Code.ToString(), " Message: ", pLib._Result.Message));
                    return;
                }
                if (checkBox1.Checked)
                {
                    button1_Click(sender, e);
                    return;
                }

                //MessageBox.Show(String.Concat("Code: ", pLib._Result.Code.ToString(), " Message: ", pLib._Result.Message));
                if (MessageBox.Show("Confirm?", "Title", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    button1_Click(sender, e);
                else
                    button4_Click_2(sender, e);

            }
            catch (ECRCommXLib.TransportException te)
            {
                MessageBox.Show(te.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        //private void button5_Click(object sender, EventArgs e)
        //{

        //}
        //public static void UC_CallBackHandler(System.Object sender,System.EventArgs  e)
        //{
        //    HEventArgs res = (HEventArgs)e;
        //    //Status.AppendText(String.Concat(Convert.ToString(res), Environment.NewLine));
        //}

        private void button6_Click(object sender, EventArgs e)
        {
            this.PAN.Text = "";
            this.HolderName.Text = "";
            //this->Amount->Text="";
            //this->AddAmount->Text="";
            this.TerminalID.Text = "";
            this.MerchantID.Text = "";
            this.BatchNumber.Text = "";
            this.IssuerName.Text = "";
            this.RRN.Text = "";
            this.Invoice.Text = "";
            this.AuthCode.Text = "";
            this.ResponseCode.Text = "";
            this.NumOfSales.Text = "";
            this.NumOfRefund.Text = "";
            this.SalesAmount.Text = "";
            this.RefundAmount.Text = "";
            this.NumOfSales_USD.Text = "";
            this.NumOfRefund_USD.Text = "";
            this.SalesAmount_USD.Text = "";
            this.RefundAmount_USD.Text = "";
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            if (pLib != null)
            {
                pLib.Cancel();
                //MessageBox.Show("Cancel OK");
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (pLib == null)
                {
                    pLib = new ECRLibcls(ECRLibcls.eComType.ETH);
                    pLib.setSocketComm(this.TCPIP.Text, Convert.ToInt32(POSPORT.Text));
                    pLib.setUserTimeOut(timeout * 1000);
                }

                if (pLib.PosSettle())
                {
                    //VND
                    this.NumOfSales.Text = pLib.getNumOfSale(704).ToString();
                    this.NumOfRefund.Text = pLib.getNumOfRefund(704).ToString();
                    this.SalesAmount.Text = pLib.getSaleAmount(704).ToString();
                    this.RefundAmount.Text = pLib.getRefundAmount(704).ToString();
                    //USD
                    this.NumOfSales_USD.Text = pLib.getNumOfSale(840).ToString();
                    this.NumOfRefund_USD.Text = pLib.getNumOfRefund(840).ToString();
                    this.SalesAmount_USD.Text = pLib.getSaleAmount(840).ToString();
                    this.RefundAmount_USD.Text = pLib.getRefundAmount(840).ToString();
                }
                else
                {
                    MessageBox.Show(pLib._Result.Message);
                }
                if (pLib._Result.Code != 0)
                {
                    MessageBox.Show(String.Concat("Not success, ", "Code: ", pLib._Result.Code.ToString(), " Message: ", pLib._Result.Message));
                }
                
            }
            catch (ECRCommXLib.TransportException te)
            {
                MessageBox.Show(te.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (pLib == null)
                {
                    pLib = new ECRLibcls(ECRLibcls.eComType.ETH);
                    pLib.setSocketComm(this.TCPIP.Text, Convert.ToInt32(POSPORT.Text));
                    pLib.setUserTimeOut(timeout * 1000);
                }
                int amount = Convert.ToInt32(this.Amount.Text);
                int add_amount = Convert.ToInt32(this.AddAmount.Text);

                if (pLib.PosPurchase(amount, add_amount, "HOST TEXT"))
                {
                    this.PAN.Text = pLib.GetPAN();
                    this.HolderName.Text = pLib.getHolderName();
                    //this.Amount.Text = (pLib.getTotalAmount()).ToString();
                    //this.AddAmount.Text = (pLib.getAddAmount()).ToString();
                    this.TerminalID.Text = pLib.getTerminalId();
                    this.MerchantID.Text = pLib.getMerchantId();
                    this.BatchNumber.Text = pLib.getBatchNum().ToString();
                    this.IssuerName.Text = pLib.getIssName();
                    this.RRN.Text = pLib.GetRRN();
                    this.Invoice.Text = pLib.getInvoiceNum();
                    this.AuthCode.Text = pLib.getAppCode();
                    this.ResponseCode.Text = pLib.getRspCode();
                }
                else
                {
                    MessageBox.Show(pLib._Result.Message);
                }
                if (pLib._Result.Code != 0)
                {
                    MessageBox.Show(String.Concat("Not success, ", "Code: ", pLib._Result.Code.ToString(), " Message: ", pLib._Result.Message));
                    return;
                }
                if (checkBox1.Checked)
                {
                    button1_Click(sender, e);
                    return;
                }
                //MessageBox.Show(String.Concat("Code: ", pLib._Result.Code.ToString(), " Message: ", pLib._Result.Message));
                if (MessageBox.Show("Confirm?", "Title", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    button1_Click(sender, e);
                else
                    button4_Click_2(sender, e);

            }
            catch (ECRCommXLib.TransportException te)
            {
                MessageBox.Show(te.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (pLib == null)
                {
                    pLib = new ECRLibcls(ECRLibcls.eComType.ETH);
                    pLib.setSocketComm(this.TCPIP.Text, Convert.ToInt32(POSPORT.Text));
                    pLib.setUserTimeOut(timeout * 1000);
                }
                int amount = Convert.ToInt32(this.Amount.Text);
                int add_amount = Convert.ToInt32(this.AddAmount.Text);

                if (pLib.PosRefund(amount, add_amount, "HOST TEXT"))
                {
                    this.PAN.Text = pLib.GetPAN();
                    this.HolderName.Text = pLib.getHolderName();
                    this.TerminalID.Text = pLib.getTerminalId();
                    this.MerchantID.Text = pLib.getMerchantId();
                    this.BatchNumber.Text = pLib.getBatchNum().ToString();
                    this.IssuerName.Text = pLib.getIssName();
                    this.RRN.Text = pLib.GetRRN();
                    this.Invoice.Text = pLib.getInvoiceNum();
                    this.AuthCode.Text = pLib.getAppCode();
                    this.ResponseCode.Text = pLib.getRspCode();
                }
                else
                {
                    MessageBox.Show(pLib._Result.Message);
                }
                if (pLib._Result.Code != 0)
                {
                    MessageBox.Show(String.Concat("Not success, ", "Code: ", pLib._Result.Code.ToString(), " Message: ", pLib._Result.Message));
                    return;
                }
                if (checkBox1.Checked)
                {
                    button1_Click(sender, e);
                    return;
                }

                //MessageBox.Show(String.Concat("Code: ", pLib._Result.Code.ToString(), " Message: ", pLib._Result.Message));
                if (MessageBox.Show("Confirm?", "Title", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    button1_Click(sender, e);
                else
                    button4_Click_2(sender, e);

            }
            catch (ECRCommXLib.TransportException te)
            {
                MessageBox.Show(te.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (pLib == null)
                {
                    pLib = new ECRLibcls(ECRLibcls.eComType.ETH);
                    pLib.setSocketComm(this.TCPIP.Text, Convert.ToInt32(POSPORT.Text));
                    pLib.setUserTimeOut(timeout * 1000);
                }
                int amount = Convert.ToInt32(this.Amount.Text);
                int add_amount = Convert.ToInt32(this.AddAmount.Text);
                int bindex = 0;
                if (comboBox1.SelectedIndex == 0)
                {
                    bindex = 704;
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    bindex = 840;
                }


                if (pLib.PosRefundMer(amount, add_amount, bindex, "HOST TEXT"))
                {
                    this.PAN.Text = pLib.GetPAN();
                    this.HolderName.Text = pLib.getHolderName();
                    //this.Amount.Text = (pLib.getTotalAmount()).ToString();
                    //this.AddAmount.Text = (pLib.getAddAmount()).ToString();
                    this.TerminalID.Text = pLib.getTerminalId();
                    this.MerchantID.Text = pLib.getMerchantId();
                    this.BatchNumber.Text = pLib.getBatchNum().ToString();
                    this.IssuerName.Text = pLib.getIssName();
                    this.RRN.Text = pLib.GetRRN();
                    this.Invoice.Text = pLib.getInvoiceNum();
                    this.AuthCode.Text = pLib.getAppCode();
                    this.ResponseCode.Text = pLib.getRspCode();
                }
                else
                {
                    MessageBox.Show(pLib._Result.Message);
                }
                if (pLib._Result.Code != 0)
                {
                    MessageBox.Show(String.Concat("Not success, ", "Code: ", pLib._Result.Code.ToString(), " Message: ", pLib._Result.Message));
                    return;
                }
                if (checkBox1.Checked)
                {
                    button1_Click(sender, e);
                    return;
                }

                //MessageBox.Show(String.Concat("Code: ", pLib._Result.Code.ToString(), " Message: ", pLib._Result.Message));
                if (MessageBox.Show("Confirm?", "Title", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    button1_Click(sender, e);
                else
                    button4_Click_2(sender, e);

            }
            catch (ECRCommXLib.TransportException te)
            {
                MessageBox.Show(te.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (pLib != null)
            {
                pLib.StopEcrMode();
                //MessageBox.Show("Confirm OK");
            }
        }

        private void cboComType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cboComType.Text == "ETH")
                {
                    pLib = new ECRLibcls(ECRLibcls.eComType.ETH);
                    //pLib.setSocketComm("192.168.2.2", 1250);
                    pLib.setSocketComm(this.TCPIP.Text, Convert.ToInt32(POSPORT.Text));
                    //pLib.setSocketComm("127.0.0.1", 1250);
                    pLib.setUserTimeOut(timeout * 1000);
                }
                else if(cboComType.Text.Length>0)
                {
                    pLib = new ECRLibcls(ECRLibcls.eComType.COM);
                    //pLib.setPortComm("COM2", "9600");
                    pLib.setPortComm(cboComType.Text, "9600");
                    pLib.setUserTimeOut(timeout * 1000);
                }
                //if (pLib == null)
                //{
                //    pLib = new ECRLibcls(ECRLibcls.eComType.COM);
                //    pLib.setPortComm("COM2", "9600");
                //    pLib.setUserTimeOut(timeout * 1000);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TCPIP_TextChanged(object sender, EventArgs e)
        {
            pLib = null;
        }

        private void POSPORT_TextChanged(object sender, EventArgs e)
        {
            pLib = null;
        }



    }
}
