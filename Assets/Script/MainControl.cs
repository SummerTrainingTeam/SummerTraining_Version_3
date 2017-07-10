using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using System.Collections.Generic;
public class MainControl : MonoBehaviour {
    ArrayList boards;
    float boardsUpSpeed = 2.0f;//屏幕刷新频率
    float speed = 5.0f;//人物活动频率
    const float MAXHEIGHT = 5.5f, MINHEIGHT = -5.5f;//客户区底端与顶端
    float currentHeight;//最上砖块的上沿
    private textset blood;//人物血量
    private scoreset score;//分数
    private coinset coin;//金币

    int randomid;//随机编号
 
    //private int HP = 100;
    void Start() {
        blood = GameObject.Find("Canvas/blood").GetComponent<textset>();
        score = GameObject.Find("Canvas/score").GetComponent<scoreset>();
        coin = GameObject.Find("Canvas/coin").GetComponent<coinset>();
  
        boards = new ArrayList();
        currentHeight = MAXHEIGHT;
        AddBoards();



    }

    void AddBoards() {
        while (currentHeight > MINHEIGHT) {
            score.score++;
            boardsUpSpeed += 0.1f;
            randomid = Random.Range(1, 7);
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            switch (randomid)
            {
                case 1: { obj.name = "cube1";break; }   //普通
                case 2: { obj.name = "cube2"; break; }  //带刺
                case 3: { obj.name = "cube3"; break; }  //作虎
                case 4: { obj.name = "cube4"; break; }  //优化
                case 5: { obj.name = "cube5"; break; }  //碎
                default: { obj.name = "cube6"; break; } //弹簧
            }
        
            float x;
            float randomnum;
            //if (boards.Count == 0)
                x = Random.Range(-4.0f, 4.0f);
            /*else
            {
                randomnum = Random.Range(-4.0f, 4.0f);
                int t = boards.Count - 1;
                GameObject Obj = (GameObject)boards[t];
                float temp = Obj.transform.position.x;
                x = Random.Range(temp - randomnum, temp + randomnum);
            }*/
            float y = Random.Range(-5.0f, -3.0f);
            currentHeight += y;
            obj.transform.localScale = new Vector3(3.0f, 0.5f, 1.0f);
            obj.transform.position = new Vector3(x, currentHeight, 0);

            boards.Add(obj);

        }
    }
    // Update is called once per frame
    void Update() {
        if (transform.position.y<-5.5f || transform.position.y>5.5f)
        {
            //结尾动画
            GameObject.Find("ObjectName").GetComponent<MainControl>().enabled = false;
        }


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        BoardsUp();
        blood.Show();
        coin.Show();
    }
    void BoardsUp() {
        currentHeight += boardsUpSpeed * Time.deltaTime;
        for (int i = 0; i < boards.Count;) {
   
            GameObject obj = (GameObject)boards[i];

            obj.transform.Translate(new Vector3(0, 1, 0) * boardsUpSpeed * Time.deltaTime);
            if (obj.transform.position.y > MAXHEIGHT) {
                boards.Remove(obj);
                Destroy(obj);
            } else {
                i++;
            }
        }
        AddBoards();
    }
    
    void OnCollisionEnter(Collision thing)
    {

        
        var name = thing.collider.name;
        //Debug.Log("Thing is " + name);
        if (name == "cube2")
        {
            blood.add();
        }
        else if (name == "cube3")
        {
            //Destroy(thing.collider);
             
        }
        else if (name == "cube4")
        {

        }
        else if (name == "cube5")
        {
            boards.Remove(thing.gameObject);
            Destroy(thing.gameObject);
           
        }
        else if (name == "cube6")
        {
            transform.Translate(new Vector3(0,6,0) * speed * Time.deltaTime);
        }  

    }

    void OnCollisionExit(Collision thing)
    {
       
    }

    void OnCollisionStay(Collision thing)
    {
        var name = thing.collider.name;
        //Debug.Log("Thing is " + name);
        if (name == "cube2")
        {
     
        }
        else if (name == "cube3")
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime*0.3f);
        }
        else if (name == "cube4") 
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime*0.3f);
        }
        else if (name == "cube5") 
        {
            //
        }
    }

}
