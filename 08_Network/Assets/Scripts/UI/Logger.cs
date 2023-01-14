using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Logger : MonoBehaviour
{
    public Color warningColor;   // 경고용 색상
        
    public Color criticalColor;  // 심각한 강조용 색상

    /// <summary>
    /// 한번에 출력되는 최대 줄 수
    /// </summary>
    public int maxLineCount = 20;

    /// <summary>
    /// 로그창에 출력될 모든 문자열들.
    /// </summary>
    List<string> logLines;

    /// <summary>
    /// 문자열을 합치기 위한 StringBuilder의 인스턴스
    /// </summary>
    StringBuilder builder;

    /// <summary>
    /// 글자 출력용 UI
    /// </summary>
    TextMeshProUGUI log;

    private void Awake()
    {
        log = GetComponentInChildren<TextMeshProUGUI>();

        logLines = new List<string>(maxLineCount + 5);      // 만약을 대비해서 5개의 여유분 추가
        builder = new StringBuilder(logLines.Capacity);     // 아무리 커져도 logLines 크기를 넘어서지 않기 때문에
    }

    private void Start()
    {
        Clear();    // 시작할 때 모두 비우기
    }

    /// <summary>
    /// 로거에 문장을 추가하는 함수
    /// </summary>
    /// <param name="logstr">추가할 문장</param>
    public void Log(string logstr)
    {
        // [] 사이에 있는 글자는 critical 색상으로 보여주기
        // {} 사이에 있는 글자는 warning 색상으로 보여주기
        //string test = string.Format("{0} 입력됨", logstr);
        //int index = logstr.IndexOf("[");                    // 문자역 안에 특정 문자열이 있는 위치 찾기
        //test = $"{logstr}에서 [는 {index}번째에 있다";
        //string[] split = logstr.Split('[', ']');            // 문자열에서 특정 문자열이 있는 부분을 기준으로 나누기
        //test = split[0] + "<#ff0000>" + split[1] + "</color>" + split[2];
        //test = logstr.Replace("[", "<#ff0000>");            // 문자열에서 특정 문자열을 다른 문자열로 변경하기
        //test += test.Replace("]", "</color>");
        //logstr = test;

        logstr = Emphasize(logstr, '[', ']', criticalColor);   // 괄호 내부를 강조
        logstr = Emphasize(logstr, '{', '}', warningColor);
        
        
        logLines.Add(logstr);               // 리스트에 문장 추가하고
        if (logLines.Count > maxLineCount)  // 최대 줄 수를 넘어서면
        {
            logLines.RemoveAt(0);           // 첫번째 줄 삭제하기
        }

        builder.Clear();                    // 빌더 클리어 
        foreach (var line in logLines)      // 빌더에 리스트에 들어있는 문장 추가
        {
            // Append : 이어서 붙임
            // AppendLine : 엔터를 친 다음 붙임
            builder.AppendLine(line);
        }

        log.text = builder.ToString();      // 빌더에 있는 내용을 하나의 문자열로 합치기
    }

    /// <summary>
    /// 원문에 지정된 괄호가 정확하게 들어있는지 확인하는 함수
    /// </summary>
    /// <param name="source">원문</param>
    /// <param name="open">여는 괄호</param>
    /// <param name="close">닫는 괄호</param>
    /// <returns>정확하게 괄호가 들어있으면 true, 아니면 false</returns>
    bool IsPair(string source, char open, char close)
    {
        // 정확하게 들어간 괄호의 조건
        // 1. 열리면 닫혀야 한다.
        // 2. 연속해서 여는 것은 안된다.

        bool result = true;     // 최종적으로 리턴할 변수
        int count = 0;          // 괄호의 수(열리는 것, 닫히는 것 모두 포함). 괄호가 열리거나 닫힐 때마다 1씩 증가
        for (int i = 0; i < source.Length; i++)         // 원문을 전부 확인하기 위한 for문
        {
            if(source[i] == open || source[i] == close) // 원문의 특정 위치가 괄호인지 확인
            {
                count++;                                // 괄호면 count 1증가
                if (count %2 == 1)                      // count가 홀수인지 짝수인지 확인
                {
                    // 홀수면 괄호가 열리는 타이밍
                    if (source[i] != open)  // 여는 괄호가 아니면
                    {
                        result = false;     // 정확하게 들어간 괄호가 아니다.
                        break;              // 이미 조건 하나가 잘못되었다면 뒤는 확인할 필요 없으니 break로 캔슬
                    }
                }
                else
                {
                    // 짝수면 괄호가 닫히는 타이밍
                    if (source[i] != close) // 그런데 닫히는 괄호가 아니면
                    {
                        result = false;     // 정확하게 들어간 괄호가 아니다.
                        break;              // 이미 조건 하나가 잘못되었다면 뒤는 확인할 필요 없으니 break로 캔슬
                    }
                }
            }
        }

        // count가 0이면 괄호가 없는 것. 
        // count가 홀수면 괄호가 짝이 안맞다는 소리
        if (count == 0 || count % 2 != 0)   
        {
            result = false; // 괄호가 없거나 짝이 맞지 않은 것도 정확하지 않은 것으로 처리
        }

        // 괄호는 열리면 무조건 닫혀야 한다.
        // 괄호는 두번 연속으로 열리는 것도 안된다.
        return result;
    }

    /// <summary>
    /// 로그 내용 중 괄호 사이에 있는 내용의 색상을 변경해서 강조하는 함수
    /// </summary>
    /// <param name="source">원문</param>
    /// <param name="open">여는 괄호 [, { 등등</param>
    /// <param name="close">닫는 괄호 ], } 등등</param>
    /// <param name="color">강조될 부분의 색상</param>
    /// <returns>강조된 부분에 색상 태그가 추가된 문자열</returns>
    string Emphasize(string source, char open, char close, Color color)
    {
        string result = source;         // 원문을 복사하기
        // 이 문자열에 괄호([], {})가 쌍으로 열리고 닫혀있는지 확인(쌍으로 열리고 닫혔다면 괄호가 존재하는 것도 확정 지을 수 있다.)
        if (IsPair(source, open, close))
        {
            // result에 괄호가 있고 정상적으로 열리고 닫혔다. => 추가 처리 필요
            string[] split = result.Split(open, close);                 // 괄호 부분을 기준으로 문장을 나누어서 따로 저장
            string colorText = ColorUtility.ToHtmlStringRGB(color);     // Color를 16진수 RGB 문자열로 변경. 나중에 사용하기 위해 미리 변경
            result = "";    // 원문은 이미 다 사용했기 때문에 비우고 새로운 문장을 만들기 위해 초기화
            for (int i = 0; i < split.Length; i++)                      // 나누어 놓았던 것을 모두 처리하기 위해 for 돌리기
            {
                result += split[i];                     // 나누어 놓았던 문장(split)을 result에 저장
                if (i + 1 != split.Length)              // 마지막 split이 아닌 경우
                {
                    if (i % 2 == 0)
                    {
                        result += $"<#{colorText}>";    // 짝수면 태그 시작부분을 result에 추가하기
                    }
                    else
                    {
                        result += "</color>";           // 홀수면 태그의 끝 부분을 result에 추가하기
                    }

                }
            }
        }
        return result;      // IsPair가 false면 원문 그대로, true면 색상 태그가 추가된 채로 리턴
    }

    /// <summary>
    /// 데이터 클리어용 함수
    /// </summary>
    public void Clear()
    {
        log.text = "";
        logLines.Clear();
        builder.Clear();
    }
}
