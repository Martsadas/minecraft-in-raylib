// Import the libaries
using System;
using System.Collections.Generic;
using Raylib_cs;
using SimplexNoise;

namespace minecraft_in_raylib
{
  // Block face class
  class BlockFace
  {
    public System.Numerics.Vector3 Pos;
    public int Dir;
    public Texture2D Tex;
    public BlockFace(float x, float y, float z, int d, Texture2D t)
    {
      this.Pos = new System.Numerics.Vector3(x, y, z);
      this.Dir = d;
      this.Tex = t;
    }
    public void Draw()
    {
      // Draw the face
      
      // Up
      if (this.Dir == 0)
      {
        Raylib.DrawCubeTexture(this.Tex, this.Pos, 1, 0.001f, 1, new Color(250, 250, 250, 255));
      }
      // Down
      if (this.Dir == 1)
      {
        Raylib.DrawCubeTexture(this.Tex, this.Pos, 1, 0.001f, 1, new Color(153, 153, 153, 255));
      }
      // Front
      if (this.Dir == 2)
      {
        Raylib.DrawCubeTexture(this.Tex, this.Pos, 1, 1, 0.001f, new Color(230, 230, 230, 255));
      }
      // Back
      if (this.Dir == 3)
      {
        Raylib.DrawCubeTexture(this.Tex, this.Pos, 1, 1, 0.001f, new Color(230, 230, 230, 255));
      }
      // Right
      if (this.Dir == 4)
      {
        Raylib.DrawCubeTexture(this.Tex, this.Pos, 0.001f, 1, 1, new Color(204, 204, 204, 255));
      }
      // Left
      if (this.Dir == 5)
      {
        Raylib.DrawCubeTexture(this.Tex, this.Pos, 0.001f, 1, 1, new Color(204, 204, 204, 255));
      }
    }
  }
  class Program
  {
    // Load the textures
    public static Texture2D texture_grass     = Raylib.LoadTexture("textures/grass.png");
    public static Texture2D texture_grassside = Raylib.LoadTexture("textures/grassside.png");
    public static Texture2D texture_dirt      = Raylib.LoadTexture("textures/dirt.png");
    public static Texture2D texture_stone     = Raylib.LoadTexture("textures/stone.png");
    public static Texture2D texture_logside   = Raylib.LoadTexture("textures/logside.png");
    public static Texture2D texture_log       = Raylib.LoadTexture("textures/log.png");
    public static Texture2D texture_sand      = Raylib.LoadTexture("textures/sand.png");
    public static Texture2D texture_leaves    = Raylib.LoadTexture("textures/leaves.png");
    public static Texture2D texture_water     = Raylib.LoadTexture("textures/water.png");
    public static Texture2D texture_bedrock   = Raylib.LoadTexture("textures/bedrock.png");
    public static Texture2D texture_planks    = Raylib.LoadTexture("textures/planks.png");
    public static Texture2D texture_glass     = Raylib.LoadTexture("textures/glass.png");
    public static Texture2D texture_clouds    = Raylib.LoadTexture("textures/clouds.png");
    public static Texture2D texture_coalore   = Raylib.LoadTexture("textures/coalore.png");
    public static Texture2D texture_ironore   = Raylib.LoadTexture("textures/ironore.png");
    public static Texture2D texture_lava      = Raylib.LoadTexture("textures/lava.png");
    public static Texture2D texture_craft     = Raylib.LoadTexture("textures/craftingtable.png");
    public static Texture2D texture_craftside = Raylib.LoadTexture("textures/craftingtableside.png");
    public static Texture2D texture_hotbar    = Raylib.LoadTexture("textures/hotbar.png");
    public static Texture2D texture_hotbar1   = Raylib.LoadTexture("textures/hotbar1.png");
    public static Texture2D texture_crosshair = Raylib.LoadTexture("textures/crosshair.png"); 
    public static Texture2D texture_hotbarsel = Raylib.LoadTexture("textures/crosshairselect.png");

    // Weather the block is transparent
    public static bool[] TRANSPARENT = {
      true,
      false,
      false,
      false,
      false,
      false,
      true,
      false,
      false,
      true,
      true,
      false,
      false,
      true,
      false
    };

    public static float sq(float num)
    {
      return num * num;
    }

    // Draws the blocks
    public static void DrawWorld(byte[,,] world, int x, int y, int z)
    {
      // Block faces
      List<float> FacesZ_l = new List<float>();
      Dictionary<float, BlockFace> FacesZ_d = new Dictionary<float, BlockFace>();

      // World bounds
      if (x < 16) { x = 16; }
      if (y < 16) { y = 16; }
      if (z < 16) { z = 16; }
      if (x > 256 - 16) { x = 256 - 16; }
      if (y > 128 - 16) { y = 128 - 16; }
      if (z > 256 - 16) { z = 256 - 16; }

      void a(float k, BlockFace v)
      {
        while (FacesZ_d.ContainsKey(k))
        {
          k += 0.01f;
        }
        FacesZ_l.Add(k);
        FacesZ_d.Add(k, v);
      }
      
      // Loop over 31*31*31 blocks
      for (int _x = -15; _x < 16; _x++)
        for (int _z = -15; _z < 16; _z++)
          for (int _y = -15; _y < 16; _y++)
          {
            // Non-Transparent blocks
            Texture2D texup = new Texture2D();
            Texture2D texside = new Texture2D();
            Texture2D texdown = new Texture2D();

            if (world[_x + x, _y + y, _z + z] == 1) { texup = texture_grass; texside = texture_grassside; texdown = texture_dirt; }     // Grass Block
            if (world[_x + x, _y + y, _z + z] == 2) { texup = texture_dirt; texside = texture_dirt; texdown = texture_dirt; }           // Dirt
            if (world[_x + x, _y + y, _z + z] == 3) { texup = texture_stone; texside = texture_stone; texdown = texture_stone; }        // Stone
            if (world[_x + x, _y + y, _z + z] == 4) { texup = texture_log; texside = texture_logside; texdown = texture_log; }          // Wooden Log
            if (world[_x + x, _y + y, _z + z] == 5) { texup = texture_sand; texside = texture_sand; texdown = texture_sand; }           // Sand
            if (world[_x + x, _y + y, _z + z] == 7) { texup = texture_bedrock; texside = texture_bedrock; texdown = texture_bedrock; }  // Bedrock
            if (world[_x + x, _y + y, _z + z] == 8) { texup = texture_planks; texside = texture_planks; texdown = texture_planks; }     // Planks
            if (world[_x + x, _y + y, _z + z] == 11) { texup = texture_coalore; texside = texture_coalore; texdown = texture_coalore; } // Coal Ore
            if (world[_x + x, _y + y, _z + z] == 12) { texup = texture_ironore; texside = texture_ironore; texdown = texture_ironore; } // Iron Ore
            if (world[_x + x, _y + y, _z + z] == 14) { texup = texture_craft; texside = texture_craftside; texdown = texture_planks; }  // Crafting Table

            // Draw the block faces
            if (world[_x + x, _y + y, _z + z] > 0 && !TRANSPARENT[world[_x + x, _y + y, _z + z]])
            {
              // Up
              if (TRANSPARENT[world[_x + x, _y + y + 1, _z + z]])
              {
                Raylib.DrawCubeTexture(texup, new System.Numerics.Vector3(_x + x, _y + y + 0.499f, _z + z), 1, 0.001f, 1, new Color(250, 250, 250, 255));
              }
              // Down
              if (TRANSPARENT[world[_x + x, _y + y - 1, _z + z]])
              {
                Raylib.DrawCubeTexture(texdown, new System.Numerics.Vector3(_x + x, _y + y - 0.499f, _z + z), 1, 0.001f, 1, new Color(153, 153, 153, 255));
              }
              // Front
              if (TRANSPARENT[world[_x + x, _y + y, _z + z + 1]])
              {
                Raylib.DrawCubeTexture(texside, new System.Numerics.Vector3(_x + x, _y + y, _z + z + 0.499f), 1, 1, 0.001f, new Color(230, 230, 230, 255));
              }
              // Back
              if (TRANSPARENT[world[_x + x, _y + y, _z + z - 1]])
              {
                Raylib.DrawCubeTexture(texside, new System.Numerics.Vector3(_x + x, _y + y, _z + z - 0.499f), 1, 1, 0.001f, new Color(230, 230, 230, 255));
              }
              // Right
              if (TRANSPARENT[world[_x + x + 1, _y + y, _z + z]])
              {
                Raylib.DrawCubeTexture(texside, new System.Numerics.Vector3(_x + x + 0.499f, _y + y, _z + z), 0.001f, 1, 1, new Color(204, 204, 204, 255));
              }
              // Left
              if (TRANSPARENT[world[_x + x - 1, _y + y, _z + z]])
              {
                Raylib.DrawCubeTexture(texside, new System.Numerics.Vector3(_x + x - 0.499f, _y + y, _z + z), 0.001f, 1, 1, new Color(204, 204, 204, 255));
              }
            }
            // Transparent blocks
            texup = new Texture2D();
            texside = new Texture2D();
            texdown = new Texture2D();

            if (world[_x + x, _y + y, _z + z] == 6) { texup = texture_leaves; texside = texture_leaves; texdown = texture_leaves; } // Leaves
            if (world[_x + x, _y + y, _z + z] == 9) { texup = texture_water; texside = texture_water; texdown = texture_water; }    // Water
            if (world[_x + x, _y + y, _z + z] == 10) { texup = texture_glass; texside = texture_glass; texdown = texture_glass; }   // Glass
            if (world[_x + x, _y + y, _z + z] == 13) { texup = texture_lava; texside = texture_lava; texdown = texture_lava; }      // Lava

            // Draw the block faces
            if (world[_x + x, _y + y, _z + z] > 0 && TRANSPARENT[world[_x + x, _y + y, _z + z]])
            {
              // Up
              if (world[_x + x, _y + y + 1, _z + z] == 0)
              {
                a(-MathF.Sqrt(sq(_x) + sq(_y + 0.499f) + sq(_z)), new BlockFace(_x + x, _y + y + 0.499f, _z + z, 0, texup));
              }
              // Down
              if (world[_x + x, _y + y - 1, _z + z] == 0)
              {
                a(-MathF.Sqrt(sq(_x) + sq(_y - 0.499f) + sq(_z)), new BlockFace(_x + x, _y + y - 0.499f, _z + z, 1, texdown));
              }
              // Front
              if (world[_x + x, _y + y, _z + z + 1] == 0)
              {
                a(-MathF.Sqrt(sq(_x) + sq(_y) + sq(_z + 0.499f)), new BlockFace(_x + x, _y + y, _z + z + 0.499f, 2, texside));
              }
              // Back
              if (world[_x + x, _y + y, _z + z - 1] == 0)
              {
                a(-MathF.Sqrt(sq(_x) + sq(_y) + sq(_z - 0.499f)), new BlockFace(_x + x, _y + y, _z + z - 0.499f, 3, texside));
              }
              // Right
              if (world[_x + x + 1, _y + y, _z + z] == 0)
              {
                a(-MathF.Sqrt(sq(_x + 0.499f) + sq(_y) + sq(_z)), new BlockFace(_x + x + 0.499f, _y + y, _z + z, 4, texside));
              }
              // Left
              if (world[_x + x - 1, _y + y, _z + z] == 0)
              {
                a(-MathF.Sqrt(sq(_x - 0.499f) + sq(_y) + sq(_z)), new BlockFace(_x + x - 0.499f, _y + y, _z + z, 5, texside));
              }
            }
          }
      
      FacesZ_l.Sort();
      
      for (int i = 0; i < FacesZ_l.Count; i++)
      {
        BlockFace f;
        FacesZ_d.TryGetValue(FacesZ_l[i], out f);

        f.Draw();
      }
    }
    
    static void Main(string[] args)
    {
      // Initilize the window
      Raylib.SetConfigFlags(Raylib_cs.ConfigFlags.FLAG_WINDOW_RESIZABLE);
      Raylib.InitWindow(800, 600, "");
      Raylib.SetWindowMinSize(400, 300);

      //Raylib.SetTargetFPS(150);

      float time = 0;

      // Setup the 3D camera
      Camera3D cam = new Camera3D();
      cam.position = new System.Numerics.Vector3(10, 10, 10);
      cam.fovy = 70;
      cam.target = new System.Numerics.Vector3(0, 1, 0);
      cam.up = new System.Numerics.Vector3(0, 1, 0);

      Raylib.SetCameraMode(cam, Raylib_cs.CameraMode.CAMERA_ORBITAL);

      Random rng = new Random();

      // Variables
      float RotationX = 0;
      float RotationY = -180;

      byte SelectedBlock = 1;

      float X = 8;
      float Y = 8;
      float Z = 8;

      byte[,,] world = new byte[258, 130, 258];

      bool altHotbar = false;

      int ScreenWidth = 0;
      int ScreenHeight = 0;

      float dist = 0;

      int _x = 16;
      int height = 64;
      int _z = 16;

      // World generator
      for (_x = 1; _x < 257; _x++)
      {
        for (_z = 1; _z < 257; _z++)
        {
          height = (int)MathF.Round((SimplexNoise.Noise.CalcPixel2D((int)MathF.Round(_x), (int)MathF.Round(_z), 0.007f) * 0.025f) * (SimplexNoise.Noise.CalcPixel2D((int)MathF.Round(_x), (int)MathF.Round(_z), 0.015f) * 0.02f) - 0.08f) + 12;
          height    += (int)MathF.Round((SimplexNoise.Noise.CalcPixel2D((int)MathF.Round(_x), (int)MathF.Round(_z), 0.007f) * 0.007f) * (SimplexNoise.Noise.CalcPixel2D((int)MathF.Round(_x), (int)MathF.Round(_z), 0.009f) * 0.02f) - 0.08f) + 12;

          for (int _y = 1; _y < 129; _y++)
          {
            
            if (_y < height)
            {
              if (SimplexNoise.Noise.CalcPixel3D(_x, _y, _z, 0.07f) > 75f)
              {
                world[_x, _y, _z] = 3;
              }
            }
            else if (_y < height + 2)
            {
              world[_x, _y, _z] = 2;
            }
            else if (_y == height + 2)
            {
              if (height > 31)
              {
                world[_x, _y, _z] = 1;
              }
              else
              {
                world[_x, _y, _z] = 5;
              }
            }
            else 
            {
              if (_y < 34)
              {
                world[_x, _y, _z] = 9;
              }
              else 
              { 
                world[_x, _y, _z] = 0; 
              }
            }

          }

          
          world[_x, 1, _z] = 7;

          if (SimplexNoise.Noise.CalcPixel2D((int)_x, (int)_z, 0.03f) %2 < .01f && _x > 5 && _x < 250 && _z > 5 && _z < 250 && height > 31)
          {
            int h = rng.Next(5, 7);

            for (int _ = 1; _ < h + 1; _++) { world[_x-2, height + _, _z] = 4; }

            for (int k = -4; k < 3; k++)
            {
              for (int j = -2; j < 3; j++)
              {
                for (int l = 0; l < 3; l++)
                {
                  if (rng.Next(1,9) >1 ) { world[k + _x, h + height + l, j + _z] = 6; } 
                }
              }
            }
          }
        }
      }

      //if ((int)(_x / 16) == _x / 16 && (int)(_z / 16) == _z / 16)
      //{
      //for (int _ = 0; _ < 20; _++)
      //{
      //  int ___ = rng.Next(1, 16);
      //  int __x = rng.Next(1, 16);
      //  int __y = rng.Next(1, height);
      //  int __z = rng.Next(1, 16);
      //
      //  for (int __ = 0; __ < ___; _++)
      //  {
      //    int ___x = rng.Next(__x + _x - 1, __x + _x + 1);
      //    int ___y = rng.Next(__y      - 1, __y      + 1);
      //    int ___z = rng.Next(__z + _z - 1, __z + _z + 1);
      //
      //    try
      //    {
      //      if (world[___x, ___y, ___z] == 3)
      //      {
      //        world[___x, ___y, ___z] = 11;
      //      }
      //    }
      //    catch { }
      //  }
      //}
      //
      //for (int _ = 0; _ < 15; _++)
      //{
      //  int ___ = rng.Next(1, 16);
      //  int __x = rng.Next(1, 16);
      //  int __y = rng.Next(1, height);
      //  int __z = rng.Next(1, 16);
      //
      //  for (int __ = 0; __ < ___; _++)
      //  {
      //    int ___x = rng.Next(__x + _x - 1, __x + _x + 1);
      //    int ___y = rng.Next(__y      - 1, __y      + 1);
      //    int ___z = rng.Next(__z + _z - 1, __z + _z + 1);
      //
      //    try
      //    {
      //      if (world[___x, ___y, ___z] == 3)
      //      {
      //        world[___x, ___y, ___z] = 12;
      //      }
      //    }
      //    catch { }
      //  }
      //}
      //}

      // Main Loop
      while (!Raylib.WindowShouldClose())
      {
        time += 0.05f;

        ScreenWidth = Raylib.GetScreenWidth();
        ScreenHeight = Raylib.GetScreenHeight();

        if (Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.KEY_RIGHT_CONTROL))
        {

          if (Raylib.IsCursorHidden())
          {
            Raylib.ShowCursor();
          }
          else
          {
            Raylib.HideCursor();
          }
        }

        if (Raylib.IsCursorHidden())
        {
          RotationX -= (Raylib.GetMouseX() - ScreenWidth) / 20f;
          RotationY -= (Raylib.GetMouseY() - 200) / 90f;

          Raylib.SetMousePosition(ScreenWidth / 2, 200 / 2);
          Raylib.SetMouseOffset(ScreenWidth / 2, 200 / 2);
        }

        // Player movement
        if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_W)) { X += MathF.Sin((RotationX) / 90) / 25; Z += MathF.Cos((RotationX) / 90) / 25; }
        if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_S)) { X -= MathF.Sin((RotationX) / 90) / 25; Z -= MathF.Cos((RotationX) / 90) / 25; }
        if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_A)) { X += MathF.Cos((RotationX) / 90) / 25; Z -= MathF.Sin((RotationX) / 90) / 25; }
        if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_D)) { X -= MathF.Cos((RotationX) / 90) / 25; Z += MathF.Sin((RotationX) / 90) / 25; }

        // Select a block
        if (!altHotbar)
        {
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_ONE)) { SelectedBlock = 1; }
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_TWO)) { SelectedBlock = 2; }
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_THREE)) { SelectedBlock = 3; }
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_FOUR)) { SelectedBlock = 4; }
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_FIVE)) { SelectedBlock = 5; }
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_SIX)) { SelectedBlock = 6; }
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_SEVEN)) { SelectedBlock = 8; }
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_EIGHT)) { SelectedBlock = 10; }
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_NINE)) { SelectedBlock = 9; }
        }
        else
        {
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_ONE)) { SelectedBlock = 11; }
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_TWO)) { SelectedBlock = 12; }
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_THREE)) { SelectedBlock = 7; }
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_FOUR)) { SelectedBlock = 14; }
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_FIVE)) { SelectedBlock = 5; }
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_SIX)) { SelectedBlock = 6; }
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_SEVEN)) { SelectedBlock = 8; }
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_EIGHT)) { SelectedBlock = 9; }
          if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_NINE)) { SelectedBlock = 13; }
        }

        // Up / Down
        if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_LEFT_SHIFT)) { Y -= 0.035f; }
        if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_SPACE)) { Y += 0.035f; }

        if (Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.KEY_TAB)) { altHotbar = !altHotbar; }

        cam.projection = Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_O) ? CameraProjection.CAMERA_ORTHOGRAPHIC : CameraProjection.CAMERA_PERSPECTIVE;

        dist = 0;

        // Raycast
        try
        {
          while (world[(int)MathF.Round(dist * MathF.Sin((RotationX) / 90)) + (int)MathF.Round(X), (int)MathF.Round(dist * 10 * MathF.Cos((RotationY / 120))) + (int)MathF.Round(Y), (int)MathF.Round(dist * MathF.Cos((RotationX) / 90)) + (int)MathF.Round(Z)] == 0 || dist < 10)
          { dist+=.05f; if (world[(int)MathF.Round(dist * MathF.Sin((RotationX) / 90)) + (int)MathF.Round(X), (int)MathF.Round(dist * 10 * MathF.Cos((RotationY / 120))) + (int)MathF.Round(Y), (int)MathF.Round(dist * MathF.Cos((RotationX) / 90)) + (int)MathF.Round(Z)] > 0) { break; } }

          if (Raylib.IsMouseButtonPressed(Raylib_cs.MouseButton.MOUSE_LEFT_BUTTON))
          { dist += .05f; world[(int)MathF.Round(dist * MathF.Sin((RotationX) / 90)) + (int)MathF.Round(X), (int)MathF.Round(dist * 10 * MathF.Cos((RotationY / 120))) + (int)MathF.Round(Y), (int)MathF.Round(dist * MathF.Cos((RotationX) / 90)) + (int)MathF.Round(Z)] = 0; }
          
          if (Raylib.IsMouseButtonPressed(Raylib_cs.MouseButton.MOUSE_RIGHT_BUTTON))
          { dist-=.05f;  world[(int)MathF.Round(dist * MathF.Sin((RotationX) / 90)) + (int)MathF.Round(X), (int)MathF.Round(dist * 10 * MathF.Cos((RotationY / 120))) + (int)MathF.Round(Y), (int)MathF.Round(dist * MathF.Cos((RotationX) / 90)) + (int)MathF.Round(Z)] = SelectedBlock; }

        }

        catch { }

        cam.target = new System.Numerics.Vector3((5 * MathF.Sin((RotationX) / 90)) + X, (50 * MathF.Cos((RotationY / 120))) + Y, (5 * MathF.Cos((RotationX) / 90) + Z));
        cam.position = new System.Numerics.Vector3(X, Y, Z);

        Raylib.BeginDrawing();
        Raylib.ClearBackground(Raylib_cs.Color.SKYBLUE);

        Raylib.BeginMode3D(cam);

        DrawWorld(world, (int)MathF.Round(X), (int)MathF.Round(Y), (int)MathF.Round(Z));

        // Clouds
        Raylib.DrawCubeTexture(texture_clouds, new System.Numerics.Vector3(time % 256,       128, 0),   256, 0.1f, 256, new Color(255, 255, 255, 255));
        Raylib.DrawCubeTexture(texture_clouds, new System.Numerics.Vector3(time % 256 - 256, 128, 0),   256, 0.1f, 256, new Color(255, 255, 255, 255));
        Raylib.DrawCubeTexture(texture_clouds, new System.Numerics.Vector3(time % 256,       128, 256), 256, 0.1f, 256, new Color(255, 255, 255, 255));
        Raylib.DrawCubeTexture(texture_clouds, new System.Numerics.Vector3(time % 256 - 256, 128, 256), 256, 0.1f, 256, new Color(255, 255, 255, 255));
        
        try
        {
          // Under water
          if (world[(int)X, (int)(Y-.5f), (int)Z] == 9)
          {
            Raylib.DrawRectangle(0, 0, ScreenWidth, ScreenHeight, new Color(0, 0, 255, 225));
          }
        }
        catch { }

        Raylib.EndMode3D();

        Raylib.DrawTexture(texture_crosshair, (ScreenWidth / 2) - 16, (ScreenHeight / 2) - 16, new Color(255, 255, 255, 255));

        if (!altHotbar)
        {
          Raylib.DrawTexture(texture_hotbar, (ScreenWidth / 2) - (9 * 66 / 2), ScreenHeight - 66, new Color(255, 255, 255, 255));
        }
        else
        {
          Raylib.DrawTexture(texture_hotbar1, (ScreenWidth / 2) - (9 * 66 / 2), ScreenHeight - 66, new Color(255, 255, 255, 255));
        }

        Raylib.EndDrawing();
      }

      // Unload the textures
      Raylib.UnloadTexture(texture_grass);
      Raylib.UnloadTexture(texture_grassside);
      Raylib.UnloadTexture(texture_dirt);
      Raylib.UnloadTexture(texture_stone);
      Raylib.UnloadTexture(texture_logside);
      Raylib.UnloadTexture(texture_log);
      Raylib.UnloadTexture(texture_sand);
      Raylib.UnloadTexture(texture_leaves);
      Raylib.UnloadTexture(texture_hotbar);
      Raylib.UnloadTexture(texture_water);
      Raylib.UnloadTexture(texture_clouds);
      Raylib.UnloadTexture(texture_crosshair);
      Raylib.UnloadTexture(texture_hotbarsel);
      Raylib.UnloadTexture(texture_hotbar1);
      Raylib.UnloadTexture(texture_coalore);
      Raylib.UnloadTexture(texture_ironore);
      Raylib.UnloadTexture(texture_lava);

      Raylib.CloseWindow();
    }
  }
}
