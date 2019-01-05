using System;
using System.Windows.Forms;
using GTA;
using GTA.Native;


public class Heli_Plane_Propeller_Speed : Script
{
    private Ped PlayerPed;
    private Vehicle vehicle;
    private ScriptSettings config;
 
    private bool Heli_Plane_Propeller_SpeedOn = false;
    private bool OnHeli_Plane_Propeller_SpeedOf;
    private float BladeSpeed = 0f;
    private Keys SpeedMin;
    private Keys SpeedPlus;

    public Heli_Plane_Propeller_Speed()
    {
        base.Tick += this.OnTick;
        base.KeyDown += this.OnKeyDown;
        this.ReadINI();
      
    }


    private void ReadINI()
    {
        
            this.config = ScriptSettings.Load("scripts\\Heli_Plane_Propeller_Speed.ini");
            this.SpeedMin = this.config.GetValue<Keys>("Options", "SpeedMin", Keys.NumPad1);
            this.SpeedPlus = this.config.GetValue<Keys>("Options", "SpeedPlus", Keys.NumPad3);
        
     
    }


    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        this.ReadINI();
        this.OnHeli_Plane_Propeller_SpeedOf = !this.OnHeli_Plane_Propeller_SpeedOf;
        bool flag = e.KeyCode == this.SpeedPlus;
        if (flag)
        {
            bool flag2 = (double)this.BladeSpeed < 1.0;
            if (flag2)
            {
                this.BladeSpeed += 2f * Game.LastFrameTime;
            }
            this.Heli_Plane_Propeller_SpeedOn = !this.Heli_Plane_Propeller_SpeedOn;
        }
        bool flag3 = e.KeyCode == this.SpeedMin;
        if (flag3)
        {
            bool flag4 = (double)this.BladeSpeed > 0.0;
            if (flag4)
            {
                this.BladeSpeed -= 2f * Game.LastFrameTime;
            }
            this.Heli_Plane_Propeller_SpeedOn = !this.Heli_Plane_Propeller_SpeedOn;
        }
    }


    private void OnTick(object sender, EventArgs e)
    {
        this.PlayerPed = Game.Player.Character;
        bool flag = this.PlayerPed.IsInVehicle();
        if (flag)
        {
            this.vehicle = this.PlayerPed.CurrentVehicle;
            bool isVehicle = this.vehicle.Model.IsVehicle;
            if (isVehicle)
            {
                Function.Call(Hash._0xFD280B4D7F3ABC4D, new InputArgument[]
                {
                    this.PlayerPed.CurrentVehicle,
                    this.BladeSpeed
                });
            }
            bool flag2 = !this.vehicle.IsDriveable;
            if (flag2)
            {
                this.Heli_Plane_Propeller_SpeedOn = false;
            }
        }
        else
        {
            this.Heli_Plane_Propeller_SpeedOn = false;
        }
    }
}
