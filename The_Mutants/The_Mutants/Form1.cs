using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Mutants
{

    public partial class Level_1 : Form
    {
        public Level_1()
        {
            InitializeComponent();

        }
        bool goup;
        bool godown;
        bool goleft;
        bool goright;
        string facing = "up";
        double playerHealth = 100;
        int speed = 10;
        int ammo = 10;
        int mutantSpeed = 3;
        int score = 0;
        bool gameOver = false;
        Random rnd = new Random();

        private void KeyIsDown(object sender, KeyEventArgs e)
            {
            if (gameOver) return; 

            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
                facing = "left";
                player.Image = Properties.Resources.Standing_Sprite_left_nobckg;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = true;
                facing = "right";
                player.Image = Properties.Resources.Standing_Sprite_nobckg;
            }

            if (e.KeyCode == Keys.Up)
            {
                facing = "up";
                goup = true;
                player.Image = Properties.Resources.Standing_Sprite_up_nobckg;
            }

            if (e.KeyCode == Keys.Left)
            {
                facing = "down";
                godown = true;
                player.Image = Properties.Resources.Standing_Sprite_down_nobckg;
            }

            }

            private void KeyIsUp(object sender, KeyEventArgs e)
            {
            if (gameOver) return;

            if (e.KeyCode == Keys.Left)
            {
                goright = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }

            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }

            if (e.KeyCode == Keys.Space && ammo > 0)
            {
                ammo--;
                Shoot(facing);

                if (ammo < 1)
                {
                    DropAmmo();
                }
            }
        }

            private void GameEngine(object sender, EventArgs e)
            {
            if (playerHealth > 1)
            {
                progressBar1.Value = Convert.ToInt32(playerHealth);
            }
            else
            {
                player.Image = Properties.Resources.sprite_dead;
                timer1.Stop();
                gameOver = true;
            }

            label1.Text = "   Ammo:  " + ammo;

            if (playerHealth < 20)
            {
                progressBar1.ForeColor = System.Drawing.Color.Red;
            }

            if (goleft && player.Left > 0)
            {
                player.Left -= speed;
            }

            if ( goright && player.Left + player.Width < 930)
            {
                player.Left += speed;
            }

            if (goup && player.Top > 60)
            {
                player.Top -= speed;
            }

            if (godown && player.Top + player.Height < 700)
            {
                player.Top += speed;
            }

            foreach (Control x in this.Controls)
            {
                // x is a Picture box and has the tag of ammo

                if (x is PictureBox && x.Tag == "ammo")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                        {
                        this.Controls.Remove(((PictureBox)x));

                        ((PictureBox)x).Dispose();
                    }
                }

                if (x is PictureBox && x.Tag == "bullet")
                {
                    if (((PictureBox)x).Left < 1 || ((PictureBox)x).Left > 930 || ((PictureBox)x).Top < 10 || ((PictureBox)x).Top > 700)
                    {
                        this.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                    }
                }

                if (x is PictureBox && x.Tag == "Mutant")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealth -= 1;
                    }

                    if (((PictureBox)x).Left > player.Left)
                    {
                        ((PictureBox)x).Left -= mutantSpeed;
                        ((PictureBox)x).Image = Properties.Resources.Single_Mutant_nobckg;
                    }

                    if (((PictureBox)x).Top > player.Top)
                    {
                        ((PictureBox)x).Top -= mutantSpeed;
                        ((PictureBox)x).Image = Properties.Resources.Single_Mutant_up_nobckg;
                    }

                    if (((PictureBox)x).Left < player.Left)
                    {
                        ((PictureBox)x).Left += mutantSpeed;
                        ((PictureBox)x).Image = Properties.Resources.Single_Mutant_down_nobckg;
                    }
                    if (((PictureBox)x).Top < player.Top)
                    {
                        ((PictureBox)x).Top += mutantSpeed;
                        ((PictureBox)x).Image = Properties.Resources.Single_Mutant_down_nobckg;
                    }
                }
                foreach (Control j in this.Controls)
                {
                    if ((j is PictureBox && j.Tag == "bullet") && (x.Tag == "mutant"))
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;
                            this.Controls.Remove(j);
                            j.Dispose();
                            this.Controls.Remove(x);
                            x.Dispose();
                            MakeMutants();
                        }
                    }
                }
            }
            }

            private void DropAmmo()
            {
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo1;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize; 
            ammo.Left = rnd.Next(10, 890);
            ammo.Top = rnd.Next(50, 600); 
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);
            ammo.BringToFront();
            player.BringToFront();
        }

            private void Shoot(string direct)
            {
            bullet dhoot = new Shoot();
            shoot.direction = direct;
            shoot.bulletLeft = player.Left + (player.Width / 2);
            shoot.bulletTop = player.Top + (player.Height / 2);
            shoot.mkBullet(this);
            }

            private void MakeMutants()
        {
            PictureBox Mutant = new PictureBox();
            Mutant.Tag = "Mutant";
            Mutant.Image = Properties.Resources.Single_Mutant_nobckg;
            Mutant.Left = rnd.Next(0, 900);
            Mutant.Top = rnd.Next(0, 800);
            Mutant.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Controls.Add(Mutant);
            player.BringToFront();
        }

    }
    }