using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Circuit
{
    public class CircuitMinigame : MinigameController
    {
        Wire[][] Grid = new Wire[4][];
        int[][] connected = new int[4][];
        [SerializeField] private Transform content;
        private int X = 0;
        private int Y = 0;
        private bool isInputing = true;
        
        public override void Launch()
        {
            base.Launch();
            isInputing = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            for (int i = 0; i < 4; i++)
            {
                Grid[i] =  new Wire[4];
                connected[i] = new int[4];
                Transform row = content.GetChild(i);
                for (int j = 0; j < 4; j++)
                {
                    Grid[i][j] = row.GetChild(j).gameObject.GetComponentInChildren<Wire>();
                }
            }
        }

        public override void OnNavigate(Vector2 dir)
        {
            if (dir.magnitude < 0.5f)
                isInputing = true;
            else if(isInputing)
            {
                X += Mathf.RoundToInt(Mathf.RoundToInt(Mathf.Abs(dir.y)) * Mathf.Sign(dir.y));
                Y += Mathf.RoundToInt(Mathf.RoundToInt(Mathf.Abs(dir.x)) * Mathf.Sign(dir.x));
                X = Mathf.Clamp(X, 0, 3);
                Y = Mathf.Clamp(Y, 0, 3);
                Debug.Log("X: " + X + "  Y: " + Y);
                isInputing = false;
            }
        }

        private int id = 0;
        public override void OnClick()
        {
            base.OnClick();
            Grid[X][Y].Rotate();
            if (Check())
                StartCoroutine(Win());
        }

        public override void OnRightClick()
        {
            base.OnRightClick();
            GameAbort();
        }

        IEnumerator Win()
        {
            yield return new WaitForSeconds(1f);
            audioSource.PlayOneShot(winSFX);
            yield return new WaitForSeconds(1f);
            GameClear();
        }

        public bool Check()
        {
            id++;
            connected[0][0] = id;
            DFS(0,0);
            return (Grid[0][0].Connection & 2) > 0 && connected[3][3] == id && (Grid[3][3].Connection & 8) > 0;
        }

        private Vector2[] dir = { new(1, 0), new (0,-1), new (-1,0), new (0, 1) };
        private void DFS(int x, int y)
        {
            Wire wire = Grid[x][y];
            Debug.Log($"{x}, {y}, {wire.Connection}");
            for (int i = 0; i < 4; i++)
            {
                if ((wire.Connection & (1 << i)) > 0)
                {
                    int nx = x + Mathf.RoundToInt(dir[i].x);
                    int ny = y + Mathf.RoundToInt(dir[i].y);
                    if(nx < 0 || nx == 4 || ny < 0 || ny == 4)
                        continue;
                    if (connected[nx][ny] != id)
                    {
                        connected[nx][ny] = id;
                        DFS(nx, ny);
                    }
                }
            }
        }
        
    }
}