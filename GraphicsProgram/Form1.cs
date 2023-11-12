using static System.Windows.Forms.AxHost;

namespace GraphicsProgram
{
    public partial class Form1 : Form
    {
        GraphicsHandler graphicsHandler;
        CommandParser commandParser;

        public Form1()
        {
            InitializeComponent();
            //GraphicsHandler graphicsHandler = new GraphicsHandler(pictureBox1);
            graphicsHandler = new GraphicsHandler(pictureBox1);
            commandParser = new CommandParser();
            commandParser.setGraphicsHandler(graphicsHandler);
            graphicsHandler.CircleTest();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //graphicsHandler.CircleTest();
            //graphicsHandler.CircleTest();
            //button1.Text = CommandParser.CommandSplit("Test A")[1];
            //graphicsHandler.ClearTest();
            commandParser.FullParse(textBox1.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var graphics = Graphics.FromImage(pictureBox1.Image);
            //Clear.ClearMethod(graphics);
            //graphicsHandler.ClearTest();

            //graphics.DrawLine(p, startX, startY, endX, endY);
            
        }
    }
}