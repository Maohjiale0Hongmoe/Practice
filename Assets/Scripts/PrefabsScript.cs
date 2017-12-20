using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

   public class PrefabsScript : MonoBehaviour
   {
       bool FirstSelected = false;
       GameObject mask1;
       GameObject mask2;
       public float x1, x2, y1, y2;
       float x, y;
       // LineRenderer line1;
       // Vector3 p1, p2,p3;
       Vector3 pt1, pt2;
       List<GameObject> all;
       void Start()
       {
           mask1 = GameObject.Find("Mask1");
           mask2 = GameObject.Find("Mask2");
           all = transform.GetComponent<CreatPrefabsScript>().all;
           //  CreatLine();
       }
       //创线
       /*void CreatLine()
       {
           GameObject L = new GameObject("Line1");
           line1 = L.AddComponent<LineRenderer>();
           line1.startWidth = 0.1f;
           line1.endWidth = 0.1f;
           line1.positionCount = 3;

       }
       //画线
    /*   void DrawlinkLine(float x1, float y1, float x2, float y2, stepType LinkType)
    {

        p1 = mask1.transform.position + new Vector3(0, 0, -2);
        p2 =mask2.transform.position + new Vector3(0, 0, -2);
        p3=PT(mask1.transform.position,mask2.transform.position)+ new Vector3(0, 0, -2);
        if (LinkType==stepType.one)
        {
            line1.SetPosition(0, p1);
            line1.SetPosition(1, p2);

            line1.SetPosition(2, p2);

        }
        if (LinkType==stepType.two)
        {
            line1.SetPosition(0, p1);
            line1.SetPosition(1,p3);
            line1.SetPosition(2, p2);
        }
    }
       */
       
       void Update()
       {
           if (Input.GetMouseButtonDown(0))
           {
               Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
               RaycastHit hitInfo = new RaycastHit();
               if (Physics.Raycast(ray, out hitInfo))
               {
                   x = hitInfo.transform.position.x;           
                   y = hitInfo.transform.position.y;

                   if (!FirstSelected)
                   {
                       if (hitInfo.transform.gameObject.name != "none")
                       {
                           x1 = x;
                           y1 = y;
                           mask1.transform.position = hitInfo.transform.position + new Vector3(0, 0, 1);
                           FirstSelected = true;
                       }
                   }
                   else
                   {
                       if (hitInfo.transform.gameObject.name != "none")
                       {
                           x2 = x;
                           y2 = y;
                           mask2.transform.position = hitInfo.transform.position + new Vector3(0, 0, 1);
                           Compare(mask1, mask2);
                           FirstSelected = false;
                       }
                   }
               }
           }
       }
       //进行比较选择0，1，2
       void Compare(GameObject a, GameObject b)
       {
           Vector3 ap = a.transform.position + new Vector3(0, 0, -1);
           Vector3 bp = b.transform.position + new Vector3(0, 0, -1);
           string an = GetName(ap);
           string bn = GetName(bp);

           if (ap.x == bp.x && ap.y == bp.y)
           {
               a.transform.position = new Vector3(100, 100, 0);
               b.transform.position = new Vector3(100, 100, 0);
               return;
           }
           //0
               if (ap.x == bp.x && ap.y != bp.y || ap.x != bp.x && ap.y == bp.y)
               {
                   if (an == bn && CheckX(ap, bp))
                   {
                       // DrawlinkLine(ap.x, ap.y, bp.x, bp.y, stepType.one);
                       SetEmpty(ap);
                       SetEmpty(bp);
                   }
                   else
                   {
                       //2
                       if (an == bn && CheckTwo(ap, bp))
                       {
                           //DrawlinkLine(ap.x, ap.y, bp.x, bp.y, stepType.two);
                           SetEmpty(ap);
                           SetEmpty(bp);
                       }
                   }

               }
               //1
               else if (ap.x != bp.x && ap.y != bp.y)
               {
                   if (an == bn && CheckOne(ap, bp))
                   {
                       //DrawlinkLine(ap.x, ap.y, bp.x, bp.y, stepType.two);
                       SetEmpty(ap);
                       SetEmpty(bp);
                   }
                   else
                   {
                       //2
                       if (an == bn && CheckTwo(ap, bp))
                       {
                           //DrawlinkLine(ap.x, ap.y, bp.x, bp.y, stepType.two);
                           SetEmpty(ap);
                           SetEmpty(bp);
                       }
                   }
               }
       }
       //是不是为空
       bool IsEmpty(float a, float b)
       {
           for (int i = 0; i < all.Count; i++)
           {
               if (all[i].transform.position == new Vector3(a, b, 0))
               {
                   if (all[i].name == "none")
                   {
                       return true;
                   }
               }
           }
           return false;
       }

       //设置消除
       void SetEmpty(Vector3 position)
       {
           for (int i = 0; i < all.Count; i++)
           {
               if (all[i].transform.position == position)
               {
                   all[i].GetComponent<MeshRenderer>().enabled = false;
                   all[i].name = "none";
                   all[i].GetComponentInChildren<TextMesh>().text = null;
                   //line1.SetPosition(0, new Vector3(100, 100, 0));
                   //line1.SetPosition(1, new Vector3(100, 100, 1));
                   mask1.transform.position = new Vector3(100, 100, 0);
                   mask2.transform.position = new Vector3(100, 100, 0);
               }
           }
       }

       //是否相等
       string GetName(Vector3 position)
       {
           for (int i = 0; i < all.Count; i++)
           {
               if (all[i].transform.position == position)
               {
                   return all[i].name;
               }
           }
           return null;
       }

       //0折检查
       bool CheckX(Vector3 ap, Vector3 bp)
       {
           float min = 0, max = 1;
           // 如果两点的x坐标相等，则在水平方向上扫描  
           if (ap.x == bp.x)
           {
               min = ap.y < bp.y ? ap.y : bp.y;
               max = ap.y > bp.y ? ap.y : bp.y;

               while (min < max)
               {
                   min += 1.1f;
                   if (min != max && !IsEmpty(ap.x, min))
                   {

                       return false;
                   }

               }
               return true;
           }
           // 如果两点的y坐标相等，则在竖直方向上扫描  
           else
           {
               min = ap.x < bp.x ? ap.x : bp.x;
               max = ap.x > bp.x ? ap.x : bp.x;
               while (min < max)
               {
                   min += 1.1f;
                   if (min != max && !IsEmpty(min, ap.y))
                   {
                       return false;
                   }

               }
               return true;
           }
       }

       //1折检查
       bool CheckOne(Vector3 ap, Vector3 bp)
       {
           // 测试对角点1  
           pt1 = new Vector3(ap.x, bp.y, 0);
           pt2 = new Vector3(bp.x, ap.y, 0);
           if (CheckX(ap, pt1) && CheckX(pt1, bp))
           {
               return true;
           }
           // 测试对角点2  

           if (CheckX(ap, pt2) && CheckX(pt2, bp))
           {
               return true;
           }
           return false;
       }

       /* Vector3 PT(Vector3 ap, Vector3 bp)
         {
             // 测试对角点1  
             pt1 = new Vector3(ap.x, bp.y, 0)+new Vector3(0,0,-1);
             pt2 = new Vector3(bp.x, ap.y, 0) + new Vector3(0, 0, -1);
             if (CheckX(ap, pt1) && CheckX(pt1, bp))
             {
                 return pt1;
             }
             // 测试对角点2  

             if (CheckX(ap, pt2) && CheckX(pt2, bp))
             {
                 return pt2;
             }
             return Vector3.zero;
         }
        */
       //2折检查
       bool CheckTwo(Vector3 ap, Vector3 bp)
       {
           float i;
           //向上
           for (i = ap.y + 1.1f; i <=5.5 ; i += 1.1f)
           {
               if (IsEmpty(ap.x, i))
               {
                   Vector3 pt1 = new Vector3(ap.x, i, 0);
            
                   if (CheckOne(pt1, bp))
                   {
                       return true;
                   }                
               }
               else
               {
                   break;
               }
           }
           //向下
           for (i = ap.y - 1.1f; i >=-2.2; i -= 1.1f)
           {
               if (IsEmpty(ap.x, i))
               {
                   Vector3 pt1 = new Vector3(ap.x, i, 0);

                   if (CheckOne(pt1, bp))
                   {
                       return true;
                   }
               }
               else
               {
                   break;
               }
           }
           //向右
           for (i = ap.x + 1.1f; i <=3.3; i += 1.1f)
           {
               if (IsEmpty(i, ap.y))
               {
                   Vector3 pt1 = new Vector3(i, ap.y, 0);

                   if (CheckOne(pt1, bp))
                   {
                       return true;
                   }
               }
               else
               {
                   break;
               }
           }
           //向左
           for (i = ap.x - 1.1f; i <=-4.4; i -= 1.1f)
           {
               if (IsEmpty(i, ap.y))
               {
                   Vector3 pt1 = new Vector3(i, ap.y, 0);

                   if (CheckOne(pt1, bp))
                   {
                       return true;
                   }
                  
               }
               else
               {
                   break;
               }
           }
           return false;
       }
   }
