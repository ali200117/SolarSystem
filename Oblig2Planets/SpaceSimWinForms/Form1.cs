using SpaceSimLibrary.Planets;
using SpaceSimLibrary;
using SpaceSimLibrary.Stars;
using SpaceSimLibrary.SpaceObjects;
using System.Windows.Forms;
using System.Drawing;
using static SpaceSimLibrary.Planets.Planet;

namespace SpaceSimWinForms
{
    public partial class Form1 : Form
    {
        private List<SpaceObject> planets = new List<SpaceObject>();
        private Dictionary<string, Image> planetImages = new Dictionary<string, Image>();
        private double currentTime = 0;
        private bool isZoomedIn = false;
        private SpaceObject zoomedPlanet = null;
        private Simulation simulator;
        private Dictionary<string, float> planetRotations = new();
        private Image backgroundImage;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.MouseClick += Form1_MouseClick;
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            planets.Add(new Mercury());
            planets.Add(new Venus());
            planets.Add(new Earth());
            planets.Add(new Mars());
            planets.Add(new Jupiter());
            planets.Add(new Saturn());
            planets.Add(new Uranus());
            planets.Add(new Neptune());

            foreach (var planet in planets)
            {
                string path = $"Images/{planet.Name.ToLower()}Planet.png";
                if (System.IO.File.Exists(path))
                    planetImages[planet.Name] = Image.FromFile(path);
            }

            planetImages["Sun"] = Image.FromFile("Images/sunPlanet.png");

            string[] moonNames = { "The Moon", "Phobos", "Deimos" };
            foreach (var moonName in moonNames)
            {
                string key = moonName.ToLower().Replace(" ", "");
                string path = $"Images/{key}Planet.png";
                if (System.IO.File.Exists(path))
                    planetImages[moonName] = Image.FromFile(path);
            }

            backgroundImage = Image.FromFile("Images/solarImage.jpg");

            foreach (var planet in planets)
                planetRotations[planet.Name] = 0f;

            simulator = new Simulation();
            simulator.DoTick += OnDoTick;
            simulator.Start();
        }

        private void OnDoTick(double time)
        {
            currentTime = time;

            foreach (var name in planetRotations.Keys.ToList())
                planetRotations[name] += 0.5f;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            float centerX = this.ClientSize.Width / 2f;
            float centerY = this.ClientSize.Height / 2f;

            if (backgroundImage != null)
                e.Graphics.DrawImage(backgroundImage, 0, 0, this.ClientSize.Width, this.ClientSize.Height);

            if (isZoomedIn && zoomedPlanet != null)
            {
                Image zoomImg = planetImages.ContainsKey(zoomedPlanet.Name) ? planetImages[zoomedPlanet.Name] : null;
                if (zoomImg != null)
                {
                    float angle = planetRotations.ContainsKey(zoomedPlanet.Name) ? planetRotations[zoomedPlanet.Name] : 0;
                    e.Graphics.TranslateTransform(centerX, centerY);
                    e.Graphics.RotateTransform(angle);
                    e.Graphics.DrawImage(zoomImg, -80, -80, 160, 160);

                    e.Graphics.ResetTransform();
                }

                e.Graphics.DrawString(zoomedPlanet.Name, this.Font, Brushes.White, centerX + 50, centerY);

                if (zoomedPlanet is Planet planetWithMoons && planetWithMoons.Moons != null)
                {
                    float moonScale = 0.6f;

                    foreach (var moon in planetWithMoons.Moons)
                    {
                        double moonTime = currentTime * 0.1;
                        double moonAngle = (2 * Math.PI * moonTime) / moon.OrbitalPeriod;

                        double moonDistance = moon.Name switch
                        {
                            "Phobos" => moon.Distance + 180,
                            "Deimos" => moon.Distance + 320,
                            _ => moon.Distance + 250
                        };

                        double moonX = moonDistance * Math.Cos(moonAngle);
                        double moonY = moonDistance * Math.Sin(moonAngle);

                        float drawX = (float)(moonX * moonScale) + centerX;
                        float drawY = (float)(moonY * moonScale) + centerY;

                        float orbitRadius = (float)(moonDistance * moonScale);
                        e.Graphics.DrawEllipse(Pens.DimGray, centerX - orbitRadius, centerY - orbitRadius, orbitRadius * 2, orbitRadius * 2);

                        if (planetImages.ContainsKey(moon.Name))
                            e.Graphics.DrawImage(planetImages[moon.Name], drawX - 20, drawY - 20, 40, 40);
                        else
                            e.Graphics.FillEllipse(Brushes.LightGray, drawX - 20, drawY - 20, 40, 40);
                    }
                }
            }
            else
            {
                if (planetImages.ContainsKey("Sun"))
                    e.Graphics.DrawImage(planetImages["Sun"], centerX - 25, centerY - 25, 50, 50);
                else
                    e.Graphics.FillEllipse(Brushes.Yellow, centerX - 25, centerY - 25, 50, 50);

                foreach (var planet in planets)
                {
                    float scale;
                    if (planet.Distance < 500000)
                        scale = 0.0012f;
                    else if (planet.Distance < 1000000)
                        scale = 0.0007f;
                    else if (planet.Distance < 3000000)
                        scale = 0.0003f;
                    else
                        scale = 0.00018f;

                    double angle = (2 * Math.PI * currentTime) / planet.OrbitalPeriod;
                    double x = planet.Distance * Math.Cos(angle);
                    double y = planet.Distance * Math.Sin(angle);

                    float drawX = (float)(x * scale) + centerX;
                    float drawY = (float)(y * scale) + centerY;

                    float radius = (float)(planet.Distance * scale);
                    e.Graphics.DrawEllipse(Pens.DimGray, centerX - radius, centerY - radius, radius * 2, radius * 2);

                    if (planetImages.ContainsKey(planet.Name))
                    {
                        float rotationAngle = planetRotations[planet.Name];
                        e.Graphics.TranslateTransform(drawX, drawY);
                        e.Graphics.RotateTransform(rotationAngle);
                        e.Graphics.DrawImage(planetImages[planet.Name], -8, -8, 16, 16);
                        e.Graphics.ResetTransform();
                    }
                    else
                    {
                        e.Graphics.FillEllipse(Brushes.White, drawX - 5, drawY - 5, 10, 10);
                    }

                    e.Graphics.DrawString(planet.Name, this.Font, Brushes.White, drawX + 8, drawY);
                }
            }

            string speedText = $"Speed: x{simulator.SpeedMultiplier}";
            e.Graphics.DrawString(speedText, this.Font, Brushes.White, 10, 10);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isZoomedIn = false;
                zoomedPlanet = null;
                Invalidate();
                return;
            }

            float centerX = this.ClientSize.Width / 2f;
            float centerY = this.ClientSize.Height / 2f;

            foreach (var planet in planets)
            {
                float scale;
                if (planet.Distance < 500000)
                    scale = 0.0012f;
                else if (planet.Distance < 1000000)
                    scale = 0.0007f;
                else
                    scale = 0.0003f;

                double angle = (2 * Math.PI * currentTime) / planet.OrbitalPeriod;
                double x = planet.Distance * Math.Cos(angle);
                double y = planet.Distance * Math.Sin(angle);

                float drawX = (float)(x * scale) + centerX;
                float drawY = (float)(y * scale) + centerY;

                if (Math.Abs(e.X - drawX) < 10 && Math.Abs(e.Y - drawY) < 10)
                {
                    zoomedPlanet = planet;
                    isZoomedIn = true;
                    Invalidate();
                    break;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
                simulator.SpeedMultiplier *= 2;
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
                simulator.SpeedMultiplier = Math.Max(0.1, simulator.SpeedMultiplier / 2);

            Invalidate();
        }
    }

    public class Simulation
    {
        public event Action<double> DoTick;
        private System.Windows.Forms.Timer timer;
        private double time = 0;
        public double SpeedMultiplier { get; set; } = 1.0;

        public Simulation()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 50;
            timer.Tick += (s, e) =>
            {
                time += SpeedMultiplier;
                DoTick?.Invoke(time);
            };
        }

        public void Start() => timer.Start();
        public void Stop() => timer.Stop();
    }
}
