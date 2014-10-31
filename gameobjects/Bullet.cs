using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

class Bullet : AnimatedGameObject
{
    protected Vector2 toward;
    protected Vector2 direction;
    protected float rotation;
    SpriteEffects spriteEffect;


    public Bullet() : base(10, "bullet")
    {
        this.LoadAnimation("Sprites/Rocket/spr_rocket@3", "default", true, 0.2f);
        this.PlayAnimation("default");
        this.origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
        spriteEffect = SpriteEffects.None;
        Reset();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, this.GlobalPosition, origin, rotation, 1.0f, spriteEffect);
    }

    public void CreateBullet(Vector2 dst, float posX, float posY)
    {
        this.Visible = true;
        Vector2 temp;
        Velocity = new Vector2(5, 5);
        position.X = posX;
        position.Y = posY;


        //Debug.Print("ROTATION " + rotation);

        toward = dst;

        temp.X = toward.X - position.X;
        temp.Y = toward.Y - position.Y;

        //normalise
        direction.X = temp.X / (float)Math.Sqrt((temp.X * temp.X) + (temp.Y * temp.Y));
        direction.Y = temp.Y / (float)Math.Sqrt((temp.X * temp.X) + (temp.Y * temp.Y));

        rotation = (float)(Math.Atan2(temp.Y, temp.X));
        if (rotation > 0.5 * Math.PI || rotation < -0.5 * Math.PI)
            spriteEffect = SpriteEffects.FlipVertically;
        else
            spriteEffect = SpriteEffects.None;
    }

    public override void Reset()
    {
        this.Visible = false;
        Position = new Vector2(-500, -500);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //Debug.Print(position.X + ", " + position.Y);
        position.X += direction.X * Velocity.X;
        position.Y += direction.Y * Velocity.Y;

        // check if we are outside the screen
        Rectangle screenBox = new Rectangle(0, 0, GameEnvironment.Screen.X, GameEnvironment.Screen.Y);
        if (!screenBox.Intersects(this.BoundingBox))
            this.Reset();
    }

    public void CheckEnemyCollision()
    {

    }
}
