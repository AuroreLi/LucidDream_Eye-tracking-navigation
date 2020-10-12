using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Tobii.XR;

public class RecordPos : MonoBehaviour
{
    public Transform player;

    public float recordTime = 1;
    public string recordName = "Position";

    public Color headColor, eyeColor;
    public bool isEye=true;

    private List<Vector3> positions = new List<Vector3>();

    private float timer = 0;
    private bool startRecord = false;
    private LineRenderer lineRenderer;
    private Material lineMat;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineMat = lineRenderer.material;
    }

    void Update()
    {
        //press keyboard "A" to start record
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("开始记录");
            startRecord = true;
            if(isEye)
                lineMat.color = eyeColor;
            else
                lineMat.color = headColor;
            positions.Add(player.position);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startRecord = false;
            timer = 0;
            WritePostions();
        }
        if (startRecord)
        {
            timer += Time.deltaTime;
            //Save the position per second 
            if (timer >= recordTime)
            {
                Debug.Log("记录:"+player.position);
                positions.Add(player.position);
                timer = 0;
                //Drawing the line between two point 
                if (positions.Count >= 2)
                    UpadateLineRenderer();
            }
        }

    }

    void WritePostions()
    {
        string content = "";
        positions.ForEach(pos=> {
        // when export the position, input space after every vector
            content += pos.ToString() + "\n";
        });

        //拼接字符串
        string path = string.Format("{0}/{1}.txt", Application.streamingAssetsPath, recordName);
        //保存文本
        File.WriteAllText(path, content);
    }
    void UpadateLineRenderer()
    {
        lineRenderer.positionCount=positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
        
    }
    private void OnDisable()
    {
        WritePostions();
    }
}
