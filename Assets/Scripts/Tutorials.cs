using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials : MonoBehaviour
{
    [SerializeField] bool hintsOn;

    bool pause
    {
        get
        {
            return Time.timeScale == 0;
        }
        set
        {
            Time.timeScale = value ? 0 : 1;
        }
    }

    public enum BoxType
    {
        rat, time, cop, punch, people, gears
    }

    [SerializeField]GameObject ratBox, timeBox, copBox, punchBox, peopleBox, gearBox;

    Dictionary<BoxType, Box> boxes = new Dictionary<BoxType, Box>();
    Queue<Box> queue = new Queue<Box>();

    private void Start()
    {
        boxes[BoxType.rat] = new Box(ratBox);
        boxes[BoxType.time] = new Box(timeBox);
        boxes[BoxType.cop] = new Box(copBox);
        boxes[BoxType.punch] = new Box(punchBox);
        boxes[BoxType.people] = new Box(peopleBox);
        boxes[BoxType.gears] = new Box(gearBox);
    }

    private void Update()
    {
        if (pause)
        {
            if (Input.GetButtonUp("Jump"))
            {
                Hide();
                if (queue.Count > 0)
                {
                    Show(queue.Dequeue());
                }
                else
                {
                    pause = false;
                }
            }
        }
    }

    private void Hide()
    {
        foreach (var item in boxes)
        {
            item.Value.box.SetActive(false);
        }
    }

    public void ShowHint(BoxType type)
    {
        if (hintsOn)
            if (!boxes[type].shown)
            {
                boxes[type].shown = true;
                if (!pause)
                {
                    Show(boxes[type]);
                }
                else
                {
                    queue.Enqueue(boxes[type]);
                }
            }
    }

    void Show(Box box)
    {
        pause = true;
        box.box.SetActive(true);
    }

    class Box
    {
        public GameObject box;
        public bool shown;
        public Action show;

        public Box(GameObject box)
        {
            this.box = box;
        }
    }
}
