using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace Mail_Suite
{
    public partial class Form1 : Form
    {

        bool html;
        bool multiple;

        public Form1()
        {
            InitializeComponent();


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (groupBox3.Visible == false) {
                groupBox3.Visible = true;
            }
            else
            {
                groupBox3.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void gmailChecked(object sender, EventArgs e)
        {
            checkBox6.Checked = false;
            checkBox5.Checked = false;
            textBox3.Text = "smtp.gmail.com";
            textBox5.Text = "587";
        }
        private void outlookChecked(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            checkBox5.Checked = false;
            textBox3.Text = "smtp-mail.outlook.com";
            textBox5.Text = "587";

        }
        private void yahooChecked(object sender, EventArgs e)
        {

            checkBox2.Checked = false;
            checkBox6.Checked = false;
            textBox3.Text = "smtp.mail.yahoo.com";
            textBox5.Text = "587";
        }

        private void button1_Click(object sender, EventArgs e)
        {


            pBar.Value = 0;

            MailMessage mail;
            SmtpClient SmtpServer;

            SmtpServer = new SmtpClient(textBox3.Text);
            SmtpServer.Port = int.Parse(textBox5.Text);
            SmtpServer.Credentials = new System.Net.NetworkCredential(textBox7.Text, textBox6.Text);
            SmtpServer.EnableSsl = true;
            mail = new MailMessage();
            mail.From = new MailAddress(textBox7.Text,senderName.Text);
           

            mail.Subject = textBox2.Text;
            mail.Body = textBox4.Text;

           
            if (textBox8.Text != String.Empty)
            {
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(textBox8.Text);
                mail.Attachments.Add(attachment);

            }

            if (html)
            {
                mail.IsBodyHtml = true;
                MessageBox.Show("html true");
            }
         
            try
            {
                int count =0,total = 0;
                pBar.Minimum = 0;
             


                if (multiple)
                {
                    groupBox6.Visible = true;
                    groupBox5.Visible = false;

                    List<string> addyList = new List<string>();
                    foreach (string line in File.ReadLines(textBox9.Text))
                    {
                        addyList.Add(line);
                        total++;
                    }
                    pBar.Maximum = total;
                    label14.Text = total.ToString();
                 
                    foreach (string address in addyList)
                    {
                        mail.To.Clear();
                        MailAddress to = new MailAddress(address);
                        mail.To.Add(to);
                        try
                        {
                            SmtpServer.Send(mail);
                            richTextBox1.AppendText("\r\n" + to + "            OK!");
                            richTextBox1.ScrollToCaret();
                            pBar.Value = count + 1;
                            count++;
                            label12.Text = count.ToString();



                        }
                        catch
                        {
                            richTextBox1.AppendText("\r\n" + to + "            FAILED!");
                            richTextBox1.ScrollToCaret();
                            count++;
                            label12.Text = count.ToString();

                        }

                    }
                    MessageBox.Show("COMPLETE");

                }
                else
                {
                    groupBox6.Visible = true;
                    groupBox5.Visible = false;
                    count = 0;
                    pBar.Maximum = 1;
                    label14.Text = "1";
                    MailAddress to = new MailAddress(textBox1.Text);
                    mail.To.Add(to);
                   
                    
                    try
                    {
                        SmtpServer.Send(mail);
                        richTextBox1.AppendText("\r\n" + to + "            OK!");
                        richTextBox1.ScrollToCaret();
                        pBar.Value = count + 1;
                        label12.Text = "1";

                    }
                    catch
                    {
                        richTextBox1.AppendText("\r\n" + to + "            FAILED!");
                        richTextBox1.ScrollToCaret();
                        label12.Text = "1";

                    }
                    MessageBox.Show("COMPLETE");



                }






            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                html = true;

            }
            else
            {
                html = false;

            }
        }

      

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            System.Windows.Forms.DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                textBox8.Text = ofd.FileName;
            }

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = textBox4.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            System.Windows.Forms.DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                textBox9.Text = ofd.FileName;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                multiple = true;

            }
            else
            {
                multiple = false;

            }
        }
    }
}
