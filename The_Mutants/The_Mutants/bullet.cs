using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace The_Mutants
{
   
    class Bullet
    {
    string bullet;
    public string direction;
    public int speed = 20;
    PictureBox AmmoBullet = new PictureBox(); 
    Timer tm = new Timer();

    public int bulletLeft;
    public int bulletTop;

        internal static void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public void MkBullet(Form form)
    {
        object Bullet = null;
        Bullet.BackColor = System.Drawing.Color.White;
        Bullet.Size = new Size(5, 5);
        Bullet.Tag = "bullet";
        Bullet.Left = bulletLeft;
        Bullet.Top = bulletTop;
        Bullet.BringToFront();
        form.Controls.Add(Bullet);

        tm.Interval = speed;
        tm.Tick += new EventHandler(tm_Tick);
        tm.Start();
    }

    public void tm_Tick(object sender, EventArgs e)
    {
        if (direction == "left")
        {
            Bullet.Left -= speed;
        }

        if (direction == "right")
        {
            Bullet.Left += speed;
        }

        if (direction == "up")
        {
            Bullet.Top -= speed; 
        }

        if (direction == "down")
        {
            Bullet.Top += speed;
        }
        if (Bullet.Left < 16 || Bullet.Left > 860 || Bullet.Top < 10 || Bullet.Top > 616)
        {
            tm.Stop();
            tm.Dispose();
            bullet.Dispose();
            object tm = null;
            object bullet = null;
        }
    }
}
