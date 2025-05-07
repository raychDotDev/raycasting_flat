/*
Copyright (c) 2004-2021, Lode Vandevenne

All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

/*
Copyright (c) 2025, Daniil Potapov 

All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using Raylib_CSharp;
using Raylib_CSharp.Windowing;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;

const int screenWidth = 640;
const int screenHeight = 480;
const int mapWidth = 24;
const int mapHeight = 24;

int[,] worldMap = new int[mapHeight, mapWidth]
{
  {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,2,2,2,2,2,0,0,0,0,3,0,3,0,3,0,0,0,1},
  {1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,3,0,0,0,3,0,0,0,1},
  {1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,2,2,0,2,2,0,0,0,0,3,0,3,0,3,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,4,0,4,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,4,0,0,0,0,5,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,4,0,4,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,4,0,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
};

double posX = 22, posY = 12;
double dirX = -1, dirY = 0;
double planeX = 0, planeY = 0.66;

Window.Init(screenWidth, screenHeight, "Raycaster");
Time.SetTargetFPS(60);

while (!Window.ShouldClose())
{
    Graphics.BeginDrawing();
    Graphics.ClearBackground(Color.Black);
    for (int x = 0; x < screenWidth; x++)
    {
        //calculate ray position and direction
        double cameraX = 2 * x / (double)screenWidth - 1;
        double rayDirX = dirX + planeX * cameraX;
        double rayDirY = dirY + planeY * cameraX;

        //which box of the map we're in
        int mapX = (int)posX;
        int mapY = (int)posY;

        double sideDistX = 0.0;
        double sideDistY = 0.0;

        //length of ray from one x or y-side to next x or y-side
        //these are derived as:
        //deltaDistX = sqrt(1 + (rayDirY * rayDirY) / (rayDirX * rayDirX))
        //deltaDistY = sqrt(1 + (rayDirX * rayDirX) / (rayDirY * rayDirY))
        //which can be simplified to abs(|rayDir| / rayDirX) and abs(|rayDir| / rayDirY)
        //where |rayDir| is the length of the vector (rayDirX, rayDirY). Its length,
        //unlike (dirX, dirY) is not 1, however this does not matter, only the
        //ratio between deltaDistX and deltaDistY matters, due to the way the DDA
        //stepping further below works. So the values can be computed as below.
        // Division through zero is prevented, even though technically that's not
        // needed in C++ with IEEE 754 floating point values.
        double deltaDistX = (rayDirX == 0) ? 1e30 : Math.Abs(1.0 / rayDirX);
        double deltaDistY = (rayDirY == 0) ? 1e30 : Math.Abs(1.0 / rayDirY);

        double perpWallDist = 0.0;

        //what direction to step in x or y-direction (either +1 or -1)
        int stepX = 0, stepY = 0;

        int hit = 0; //was there a wall hit?
        int side = 0; //was a NS or a EW wall hit?

        //calculate step and initial sideDist
        if (rayDirX < 0)
        {
            stepX = -1;
            sideDistX = (posX - mapX) * deltaDistX;
        }
        else
        {
            stepX = 1;
            sideDistX = (mapX + 1.0 - posX) * deltaDistX;
        }

        if (rayDirY < 0)
        {
            stepY = -1;
            sideDistY = (posY - mapY) * deltaDistY;
        }
        else
        {
            stepY = 1;
            sideDistY = (mapY + 1.0 - posY) * deltaDistY;
        }

        //perform DDA
        while (hit == 0)
        {
            //jump to next map square, either in x-direction, or in y-direction
            if (sideDistX < sideDistY)
            {
                sideDistX += deltaDistX;
                mapX += stepX;
                side = 0;
            }
            else
            {
                sideDistY += deltaDistY;
                mapY += stepY;
                side = 1;
            }
            //Check if ray has hit a wall
            if (worldMap[mapX, mapY] > 0) hit = 1;
        }

        //Calculate distance projected on camera direction. This is the shortest distance from the point where the wall is
        //hit to the camera plane. Euclidean to center camera point would give fisheye effect!
        //This can be computed as (mapX - posX + (1 - stepX) / 2) / rayDirX for side == 0, or same formula with Y
        //for size == 1, but can be simplified to the code below thanks to how sideDist and deltaDist are computed:
        //because they were left scaled to |rayDir|. sideDist is the entire length of the ray above after the multiple
        //steps, but we subtract deltaDist once because one step more into the wall was taken above.
        if (side == 0) perpWallDist = (sideDistX - deltaDistX);
        else perpWallDist = (sideDistY - deltaDistY);

        //Calculate height of line to draw on screen
        int lineHeight = (int)(screenHeight / perpWallDist);

        //calculate lowest and highest pixel to fill in current stripe
        int drawStart = -lineHeight / 2 + screenHeight / 2;
        if (drawStart < 0) drawStart = 0;
        int drawEnd = lineHeight / 2 + screenHeight / 2;
        if (drawEnd >= screenHeight) drawEnd = screenHeight - 1;

		//choose wall color
        Color color = Color.White;
        switch (worldMap[mapX, mapY])
        {
            case 1: color = Color.Red; break;
            case 2: color = Color.Green; break;
            case 3: color = Color.Blue; break;
            case 4: color = Color.White; break;
            default: color = Color.Yellow; break;
        }

		//give x and y sides different brightness
        if (side == 1) { color = Color.Tint(color, new(127, 127, 127, 255)); }

		//draw the pixels of the stripe as a vertical line
        Graphics.DrawLine(x, drawStart, x, drawEnd, color);
    }
    Graphics.EndDrawing();

    //speed modifiers
    double moveSpeed = Time.GetFrameTime() * 5.0;
    double rotSpeed = Time.GetFrameTime() * 3.0;

    //move forward if no wall in front of you
    if (Input.IsKeyDown(KeyboardKey.Up))
    {
        if (worldMap[(int)(posX + dirX * moveSpeed), (int)posY] == 0)
            posX += dirX * moveSpeed;
        if (worldMap[(int)posX, (int)(posY + dirY * moveSpeed)] == 0)
            posY += dirY * moveSpeed;
    }
    //move backwards if no wall behind you
    if (Input.IsKeyDown(KeyboardKey.Down))
    {
        if (worldMap[(int)(posX - dirX * moveSpeed), (int)posY] == 0)
            posX -= dirX * moveSpeed;
        if (worldMap[(int)posX, (int)(posY - dirY * moveSpeed)] == 0)
            posY -= dirY * moveSpeed;
    }
    //rotate to the left
    if (Input.IsKeyDown(KeyboardKey.Left))
    {
        double oldDirX = dirX;
        dirX = dirX * Math.Cos(rotSpeed) - dirY * Math.Sin(rotSpeed);
        dirY = oldDirX * Math.Sin(rotSpeed) + dirY * Math.Cos(rotSpeed);
        double oldPlaneX = planeX;
        planeX = planeX * Math.Cos(rotSpeed) - planeY * Math.Sin(rotSpeed);
        planeY = oldPlaneX * Math.Sin(rotSpeed) + planeY * Math.Cos(rotSpeed);
    }
    //rotate to the right
    if (Input.IsKeyDown(KeyboardKey.Right))
    {
        double oldDirX = dirX;
        dirX = dirX * Math.Cos(-rotSpeed) - dirY * Math.Sin(-rotSpeed);
        dirY = oldDirX * Math.Sin(-rotSpeed) + dirY * Math.Cos(-rotSpeed);
        double oldPlaneX = planeX;
        planeX = planeX * Math.Cos(-rotSpeed) - planeY * Math.Sin(-rotSpeed);
        planeY = oldPlaneX * Math.Sin(-rotSpeed) + planeY * Math.Cos(-rotSpeed);
    }
}
Window.Close();
