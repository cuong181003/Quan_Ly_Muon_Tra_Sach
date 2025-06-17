using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlythuvien
{
    public partial class Dashbroad : Form
    {
        string manvLogin;

        public Dashbroad(string manvLogin)
        {

            InitializeComponent();
             this.manvLogin = manvLogin;
        }

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýTrảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trasach trasach = new Trasach();
            trasach.Show();
        }

        private void quảnLýDanhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Quanlysach qlsach = new Quanlysach();
            qlsach.Show();
        }

        private void eXitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        

        private void quảnLýĐộcGiảToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

        private void cậpNhậtThôngTinĐộcGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thongtindocgia qldg = new Thongtindocgia();
            qldg.Show();
        }

        private void quảnLýLoạiSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quanlytheloaisach qltls = new Quanlytheloaisach();
            qltls.Show();
        }

        private void cấpThẻĐộcGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Capthedocgia capthedocgia = new Capthedocgia();
            capthedocgia.Show();
        }

        private void quảnLýMượnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Muonsach muonsach = new Muonsach(manvLogin);
            muonsach.Show();
        }

        private void quảnLýToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Xulyphat xulyphat = new Xulyphat(manvLogin);
            xulyphat.Show();
        }

        private void thốngKêLượtMượnSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thongkeluotmuonsach thongkeluotmuonsach = new Thongkeluotmuonsach(manvLogin);
            thongkeluotmuonsach.Show();
        }

        private void thốngKêSáchBịThấtLạcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thongkesachthatlac thongkesachthatlac = new Thongkesachthatlac(manvLogin);
            thongkesachthatlac.Show();
        }

        private void thốngKêSáchCầnThanhLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thongkesachcanthanhly thongkesachthanhly = new Thongkesachcanthanhly(manvLogin);
            thongkesachthanhly.Show();
        }

        private void thốngKêĐộcGiảTrảSáchQuáHạnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thongketrasachquahan sachquahan = new Thongketrasachquahan(manvLogin);
            sachquahan.Show();
        }


        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thongtincanhan canhan = new Thongtincanhan(manvLogin);
            canhan.Show();
        }


    }
}
