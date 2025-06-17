using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace Quanlythuvien
{
    public partial class The : Form
    {
        Connection_SQL connection = new Connection_SQL();

        public The(int soTheDG)
        {
            InitializeComponent();
            txt_sotheDG_The.Text = soTheDG.ToString();
            loadthe();
        }


        void loadthe()
        {
            DataTable table = connection.LayThongTinTheDG(int.Parse(txt_sotheDG_The.Text));

            DataRow row = table.Rows[0];

            txt_sotheDG_The.Text = row["SoTheDG"].ToString();
            txt_maDG_The.Text = row["MaDG"].ToString();
            txt_ngaycap_The.Text = ((DateTime)row["NgayCap"]).ToString("dd/MM/yyyy");
            txt_handung_The.Text = ((DateTime)row["HanDung"]).ToString("dd/MM/yyyy");
        }

        }


    }
