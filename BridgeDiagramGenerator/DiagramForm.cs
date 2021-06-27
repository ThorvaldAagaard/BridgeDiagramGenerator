using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BridgeDiagramGenerator
{
    public partial class DiagramForm : Form
    {
        public BridgeDiagram bd;

        public DiagramForm()
        {
            InitializeComponent();
            bd = new BridgeDiagram();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var a = new Diagram();
            bd.Name = DiagramName.Text;
            bd.Height = DiagramHeight.Text;
            bd.Width = DiagramWidth.Text;
            bd.Resolution = DiagramResolution.Text;
            bd.North = new string[4];
            bd.North[0] = NorthSpade.Text;
            bd.North[1] = NorthHeart.Text;
            bd.North[2] = NorthDiamond.Text;
            bd.North[3] = NorthClub.Text;
            bd.South = new string[4];
            bd.South[0] = SouthSpade.Text;
            bd.South[1] = SouthHeart.Text;
            bd.South[2] = SouthDiamond.Text;
            bd.South[3] = SouthClub.Text;
            bd.West = new string[4];
            bd.West[0] = WestSpade.Text;
            bd.West[1] = WestHeart.Text;
            bd.West[2] = WestDiamond.Text;
            bd.West[3] = WestClub.Text;
            bd.East = new string[4];
            bd.East[0] = EastSpade.Text;
            bd.East[1] = EastHeart.Text;
            bd.East[2] = EastDiamond.Text;
            bd.East[3] = EastClub.Text;
            a.bd = bd;
            a.Show();
        }
    }
}
