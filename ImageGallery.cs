using C1.Win.C1Tile;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace New_Photo_Gallery
{
    public partial class ImageGallery : Form
    {
        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private TextBox _searchBox;
        private Panel panel2;
        private PictureBox _search;
        private PictureBox _exportImage;
        private C1TileControl _imageTileControl;
        private Group group1;
        private Tile tile1;
        private Tile tile2;
        private Tile tile3;
        private StatusStrip statusStrip1;
        private C1.C1Pdf.C1PdfDocument c1PdfDocument1;
        private ToolStripProgressBar toolStripProgressBar1;
        DataFetcher datafetch = new DataFetcher();
        List<ImageItem> imagesList;
        int checkedItems = 0;
        C1.C1Pdf.C1PdfDocument imagePdfDocument = new C1.C1Pdf.C1PdfDocument();

        public ImageGallery()
        {
            PanelElement panelElement1 = new PanelElement();
            ImageElement imageElement1 = new ImageElement();
            TextElement textElement1 = new TextElement();
            this.splitContainer1 = new SplitContainer();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.panel1 = new Panel();
            this._searchBox = new TextBox();
            this.panel2 = new Panel();
            this._search = new PictureBox();
            this.statusStrip1 = new StatusStrip();
            this.toolStripProgressBar1 = new ToolStripProgressBar();
            this._imageTileControl = new C1TileControl();
            this.group1 = new Group();
            this.tile1 = new Tile();
            this.tile2 = new Tile();
            this.tile3 = new Tile();
            this._exportImage = new PictureBox();
            this.c1PdfDocument1 = new C1.C1Pdf.C1PdfDocument();

            // splitContainer1 control
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.FixedPanel = FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.SplitterDistance = 40;
            this.splitContainer1.Margin = new Padding(2, 2, 2, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Orientation.Horizontal;
            
            // splitContainer1.Panel1 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
             
            // splitContainer1.Panel2 
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel2.Controls.Add(this._imageTileControl);
            this.splitContainer1.Panel2.Controls.Add(this._exportImage);
            
            // tableLayoutPanel1 control
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.5F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 2, 0);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new Size(800, 40);
            
            // panel1
            this.panel1.Controls.Add(this._searchBox);
            this.panel1.Location = new Point(477, 0);
            this.panel1.Size = new Size(287, 40);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(287, 40);
            this.panel1.Paint += new PaintEventHandler(this.panel1_Paint);
            
            // _searchBox
            this._searchBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.panel1.Dock = DockStyle.Fill;
            this._searchBox.BorderStyle = BorderStyle.None;
            this._searchBox.Location = new Point(16, 9);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new Size(244, 16);
            this._searchBox.TabIndex = 0;
            this._searchBox.Text = "Search Image";
            
            // panel2
            this.panel2.Controls.Add(this._search);
            this.panel2.Location = new Point(479, 12);
            this.panel2.Margin = new Padding(2, 12, 45, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(40, 16);
            this.panel2.TabIndex = 1;
            
            // _search (picture box control)
            this._search.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this._search.Image = new Bitmap("images.png");
            this._search.Location = new Point(0, 0);
            this._search.Margin = new Padding(0);
            this._search.Name = "_search";
            this._search.Size = new Size(40, 16);
            this._search.SizeMode = PictureBoxSizeMode.Zoom;
            this._search.Dock = DockStyle.Left;
            this._search.Click += new EventHandler(this._search_Click);
            
            // statusStrip1
            this.statusStrip1.Items.AddRange(new ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Dock = DockStyle.Bottom;
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Visible = false;
            
            // toolStripProgressBar1
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
             
            // _imageTileControl
            this._imageTileControl.Visible = false;
            this._imageTileControl.AllowChecking = true;
            this._imageTileControl.AllowRearranging = true;
            this._imageTileControl.CellHeight = 78;
            this._imageTileControl.CellSpacing = 11;
            this._imageTileControl.CellWidth = 78;
            panelElement1.Alignment = ContentAlignment.BottomLeft;
            panelElement1.Children.Add(imageElement1);
            panelElement1.Children.Add(textElement1);
            panelElement1.Margin = new Padding(12, 6, 10, 6);
            this._imageTileControl.Location = new Point(29, 37);
            this._imageTileControl.DefaultTemplate.Elements.Add(panelElement1);
            this._imageTileControl.Groups.Add(this.group1);
            this._imageTileControl.Name = "_imageTileControl";
            this._imageTileControl.Size = new Size(764, 718);
            this._imageTileControl.SurfacePadding = new Padding(12, 4, 12, 4);
            this._imageTileControl.SwipeDistance = 20;
            this._imageTileControl.SwipeRearrangeDistance = 98;
            this._imageTileControl.TileChecked += new EventHandler<TileEventArgs>(this._imageTileControl_TileChecked);
            this._imageTileControl.TileUnchecked += new EventHandler<TileEventArgs>(this._imageTileControl_TileUnchecked);
            this._imageTileControl.Paint += new PaintEventHandler(this._imageTileControl_Paint);
            
            // group1
            this.group1.Name = "group1";
            this.group1.Tiles.Add(this.tile1);
            this.group1.Tiles.Add(this.tile2);
            this.group1.Tiles.Add(this.tile3);
             
            // tile1
            this.tile1.BackColor = Color.LightCoral;
            this.tile1.Name = "tile1";
            this.tile1.Text = "Tile 1";
            
            // tile2
            this.tile2.BackColor = Color.Teal;
            this.tile2.Name = "tile2";
            this.tile2.Text = "Tile 2";
            
            // tile3
            this.tile3.BackColor = Color.SteelBlue;
            this.tile3.Name = "tile3";
            this.tile3.Text = "Tile 3";
            
            // _exportImage
            this._exportImage.Image = new Bitmap("download.png");
            this._exportImage.Location = new Point(29, 3);
            this._exportImage.Name = "_exportImage";
            this._exportImage.Size = new Size(135, 28);
            this._exportImage.SizeMode = PictureBoxSizeMode.StretchImage;
            this._exportImage.TabIndex = 2;
            this._exportImage.TabStop = false;
            this._exportImage.Click += new EventHandler(this._exportImage_Click);
            this._exportImage.Paint += new PaintEventHandler(this._exportImage_Paint);
             
            // ImageGallery
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MaximumSize = new Size(810, 810);
            this.Size = new Size(780, 800);
            this.Name = "ImageGallery";
            this.ShowIcon = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Image Gallery";
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = _searchBox.Bounds;
            r.Inflate(3, 3);
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);

        }

        private void AddTiles(List<ImageItem> imageList)
        {
            _imageTileControl.Groups[0].Tiles.Clear();
            try
            {
                foreach (var imageitem in imageList)
                {
                    Tile tile = new Tile();
                    tile.HorizontalSize = 2;
                    tile.VerticalSize = 2;
                    _imageTileControl.Groups[0].Tiles.Add(tile);
                    Image img = Image.FromStream(new MemoryStream(imageitem.Base64));
                    Template tl = new Template();
                    ImageElement ie = new ImageElement();
                    ie.ImageLayout = ForeImageLayout.Stretch;
                    tl.Elements.Add(ie);
                    tile.Template = tl;
                    tile.Image = img;
                }
            }
            catch
            {
                Console.WriteLine("Image List is empty.");
            }
        }


        private async void _search_Click(object sender, EventArgs e)
        {
            statusStrip1.Visible = true;
            _exportImage.Visible = false;
            imagesList = await datafetch.GetImageData(_searchBox.Text);
            _imageTileControl.Visible = true;
            AddTiles(imagesList);
            statusStrip1.Visible = false;

        }

        private void ConvertToPdf(List<Image> images)
        {
            RectangleF rect = imagePdfDocument.PageRectangle;
            bool firstPage = true;
            foreach (var selectedimg in images)
            {
                if (!firstPage)
                {
                    imagePdfDocument.NewPage();
                }
                firstPage = false;
                rect.Inflate(-72, -72);
                imagePdfDocument.DrawImage(selectedimg, rect);
            }
        }

        private void _exportImage_Click(object sender, EventArgs e)
        {
            List<Image> images = new List<Image>();
            foreach (Tile tile in _imageTileControl.Groups[0].Tiles)
            {
                if (tile.Checked)
                {
                    images.Add(tile.Image);
                }
            }
            ConvertToPdf(images);
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = "pdf";
            saveFile.Filter = "PDF files (*.pdf)|*.pdf*";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                imagePdfDocument.Save(saveFile.FileName);
            }

        }

        private void _exportImage_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = new Rectangle(_exportImage.Location.X, _exportImage.Location.Y, _exportImage.Width, _exportImage.Height);
            r.X -= 29;
            r.Y -= 3;
            r.Width--;
            r.Height--;
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);
            e.Graphics.DrawLine(p, new Point(0, 43), new Point(this.Width, 43));

        }

        private void _imageTileControl_TileChecked(object sender, C1.Win.C1Tile.TileEventArgs e)
        {
            checkedItems++;
            _exportImage.Visible = true;

        }

        private void _imageTileControl_TileUnchecked(object sender, C1.Win.C1Tile.TileEventArgs e)
        {
            checkedItems--;
            if (checkedItems <= 0)
            {
                _exportImage.Visible = false;
            }
            else
                _exportImage.Visible = true;

        }

        private void _imageTileControl_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawLine(p, 0, 43, 800, 43);

        }

    }
}
