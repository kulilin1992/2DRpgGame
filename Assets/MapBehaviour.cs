using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
 
public class MapBehaviour : MonoBehaviour
{
    public Vector2Int mapSize;//地图尺寸
 
    public Tilemap tilemap;
    public Tile normalTile;//白色tile
    public Tile obstacleTile;//黑色tile
    public Tile pathTile;//绿色tile
 
    public int obstacleCount;//要生成的障碍物数量
 
    public Vector3Int startPos;//起点
    public Vector3Int endPos;//终点
 
    private bool hasStartPosSet;//是否设置了起点
    private bool hasEndPosSet;//是否设置了终点
 
    private Dictionary<Vector3Int, int> search = new Dictionary<Vector3Int, int>();//要进行的查找任务
    private Dictionary<Vector3Int, int> cost = new Dictionary<Vector3Int, int>();//起点到当前点的消耗
    private Dictionary<Vector3Int, Vector3Int> pathSave = new Dictionary<Vector3Int, Vector3Int>();//保存回溯路径
    private List<Vector3Int> hadSearch = new List<Vector3Int>();//已经查找过的坐标
 
    private List<Vector3Int> obstacle = new List<Vector3Int>();//障碍物坐标

    public List<Vector3> aa = new List<Vector3>();//测试
    public Vector3 endPointaa;
 
    private void Start()
    {
        CreateNormalTiles();
        CreateObstacleTiles();
    }
 
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!hasStartPosSet)//第一次点击设置起点
            {
                startPos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                tilemap.SetTile(startPos, pathTile);
                hasStartPosSet = true;
            }
            else if (!hasEndPosSet)//第二次点击设置终点
            {
                endPos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                tilemap.SetTile(endPos, pathTile);
                hasEndPosSet = true;
 
                AStarSearchPath();
            }
            else//重置
            {
                hasStartPosSet = false;
                hasEndPosSet = false;
 
                foreach (var item in pathSave)
                {
                    tilemap.SetTile(item.Key, normalTile);
                }
 
                search.Clear();
                cost.Clear();
                pathSave.Clear();
                hadSearch.Clear();
                aa.Clear();
 
            }
        }
    }
    //创建白色地图
    public void CreateNormalTiles()
    {
        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                Vector3Int position = new Vector3Int(i, j, 0);
                tilemap.SetTile(position, normalTile);
            }
        }
    }
    //创建黑色障碍
    public void CreateObstacleTiles()
    {
        List<Vector3Int> blankTiles = new List<Vector3Int>();
 
        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                blankTiles.Add(new Vector3Int(i, j, 0));
            }
        }
 
        for (int i = 0; i < obstacleCount; i++)
        {
            int index = Random.Range(0, blankTiles.Count);
            Vector3Int obstaclePos = blankTiles[index];
            blankTiles.RemoveAt(index);
            obstacle.Add(obstaclePos);
 
            tilemap.SetTile(obstaclePos, obstacleTile);
        }
    }
    //AStar算法查找
    public void AStarSearchPath()
    {
        //初始化
        search.Add(startPos, GetHeuristic(startPos, endPos));
        cost.Add(startPos, 0);
        hadSearch.Add(startPos);
        pathSave.Add(startPos, startPos);
 
        while (search.Count > 0)
        {
            Vector3Int current = GetShortestPos();//获取任务列表里的最少消耗的那个坐标
 
            if (current.Equals(endPos))
                break;
 
            List<Vector3Int> neighbors = GetNeighbors(current);//获取当前坐标的邻居
 
            foreach (var next in neighbors)
            {
                if (!hadSearch.Contains(next))
                {
                    cost.Add(next, cost[current] + 1);//计算当前格子的消耗，其实就是上一个格子加1步
                    search.Add(next, cost[next] + GetHeuristic(next, endPos));//添加要查找的任务，消耗值为当前消耗加上当前点到终点的距离
                    pathSave.Add(next, current);//保存路径
                    hadSearch.Add(next);//添加该点为已经查询过
                }
            }
        }
         
        if (pathSave.ContainsKey(endPos))
            ShowPath();
        else
            print("No road");
    }
    //获取周围可用的邻居
    private List<Vector3Int> GetNeighbors(Vector3Int target)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>();
 
        Vector3Int up = target + Vector3Int.up;
        Vector3Int right = target + Vector3Int.right;
        Vector3Int left = target - Vector3Int.right;
        Vector3Int down = target - Vector3Int.up;
 
        //Up
        if (up.y < mapSize.y && !obstacle.Contains(up))
        {
            neighbors.Add(up);
        }
        //Right
        if (right.x < mapSize.x && !obstacle.Contains(right))
        {
            neighbors.Add(target + Vector3Int.right);
        }
        //Left
        if (left.x >= 0 && !obstacle.Contains(left))
        {
            neighbors.Add(target - Vector3Int.right);
        }
        //Down
        if (down.y >= 0 && !obstacle.Contains(down))
        {
            neighbors.Add(target - Vector3Int.up);
        }
 
        return neighbors;
    }
    //获取当前位置到终点的消耗
    private int GetHeuristic(Vector3Int posA, Vector3Int posB)
    {
        return Mathf.Abs(posA.x - posB.x) + Mathf.Abs(posA.y - posB.y);
    }
    //获取任务字典里面最少消耗的坐标
    private Vector3Int GetShortestPos()
    {
        KeyValuePair<Vector3Int, int> shortest = new KeyValuePair<Vector3Int, int>(Vector3Int.zero, int.MaxValue);
 
        foreach (var item in search)
        {
            if (item.Value < shortest.Value)
            {
                shortest = item;
            }
        }
 
        search.Remove(shortest.Key);
 
        return shortest.Key;
    }
    //显示查找完成的路径
    private void ShowPath()
    {
        print(pathSave.Count);
        Vector3Int current = endPos;
        endPointaa = new Vector3(endPos.x, endPos.y, endPos.z);
        aa.Add(endPointaa);
        while (current != startPos)
        {
            
            Vector3Int next = pathSave[current];

            Vector3 position = new Vector3(next.x, next.y, next.z);

            aa.Add(position);

 
            tilemap.SetTile(current, pathTile);
 
            current = next;
        }
        aa.Reverse();

        foreach (var item in aa)
        {
            Debug.Log(item);
        }
    }
}