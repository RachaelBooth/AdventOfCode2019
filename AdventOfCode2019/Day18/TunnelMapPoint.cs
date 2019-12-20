using System.Collections.Generic;
using AdventOfCode2019.Shared;

namespace AdventOfCode2019.Day18
{
    public class TunnelMapPoint : Coordinate
    {
        private string key;
        private string door;
        private bool isWall;
        private bool isEntrance;

        public TunnelMapPoint(int x, int y, char content) : base(x, y)
        {
            if (content == '.')
            {
                isWall = false;
                isEntrance = false;
                key = null;
                door = null;
            } 
            else if (content == '#')
            {
                isWall = true;
                isEntrance = false;
                key = null;
                door = null;
            }
            else if (content == '@')
            {
                isWall = false;
                isEntrance = true;
                key = null;
                door = null;
            }
            else
            {
                isWall = false;
                isEntrance = false;
                if (char.IsLower(content))
                {
                    key = content.ToString();
                    door = null;
                }
                else
                {
                    key = null;
                    door = content.ToString();
                }
            }
            
        }

        public bool IsPassable(List<string> keys = null)
        {
            keys ??= new List<string>();

            if (isWall)
            {
                return false;
            }

            if (door == null)
            {
                return true;
            }

            return keys.Exists(k => k.ToUpper() == door);
        }

        public bool IsEntrance()
        {
            return isEntrance;
        }
    }
}
