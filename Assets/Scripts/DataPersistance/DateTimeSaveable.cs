using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DateTimeSaveable
{
    public int seconds;
    public int minute;
    public int hour;
    public int day;
    public int month;
    public int year;
    public DateTimeSaveable(int y, int mo, int d, int h, int mi, int s)
    {
        year = y; month = mo; day = d; hour = h; minute = mi; seconds = s;
    }
    public int Seconds
    {
        get { return seconds; }

        set { seconds = value; }
    }

    public int Minute
    {
        get { return minute; }

        set { minute = value; }
    }

    public int Hour
    {
        get { return hour; }

        set { hour = value; }
    }

    public int Day
    {
        get { return day; }

        set { day = value; }
    }

    public int Month
    {
        get
        {
            return month;
        }

        set
        {
            month = value;
        }
    }

    public int Year
    {
        get
        {
            return year;
        }

        set
        {
            year = value;
        }
    }
}

