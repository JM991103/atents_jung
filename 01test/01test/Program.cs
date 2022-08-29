using _01test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _01_Console
{
    internal class Program
    {
        // 스코프(Scope) : 변수나 함수를 사용할 수 있는 범위. 변수를 선언한 시점에서 해당 변수가 포함된 중괄호가 끝나는 구간까지
        static void Main(string[] args)
        {
            // 주말과제용

            Human player; //human 클래스의 player을 생성한다
            Console.Write("용사님의 이름을 입력해 주세요 : ");   //출력문 이름 입력은 한번만 처리
            string name = Console.ReadLine();   //string형 name에 입력받은 문구를 넣는다.

            //스테이터스 리롤 처리
            string result; //string형 result라는 변수를 생성한다.
            do
            {
                player = new Human(name);       //player를 human타입을 부여하고 name을 human클래스에 넣어준다.
                Console.WriteLine("이대로 진행하시겠습니까? (Yes / No)"); //출력문
                result = Console.ReadLine(); //result입력문
            } while (result != "yes" && result != "Yes" && result != "y" && result != "Y"); // result 입력받은 값이 yes Yes y Y가 아닐때 do - while문을 실행
            //while(!(result == "yes" ||result == "Yes" ||result == "y" ||result == "Y" ||))


            // 적 만들기
            Orc enemy = new Orc("오크"); //Orc의 이름enemy를 선언과 동시에 orc타입을 부여한다. 오크라는 이름을 orc클래스에 넣어줌
            Console.WriteLine($"{enemy.Name}가 나타났다.");//출력문

            Console.WriteLine("\n\n--------------------전투시작--------------------\n\n"); //출력문


            while(true) //while문 ture 참값이 나오면 무한 루프(한명이 죽으면 break로 종료)
            {
                int selection;
                do
                {
                    Console.WriteLine("행동을 선택하세요 1.공격 2.스킬 3.방어");
                    string temp = Console.ReadLine();  //글자를 받아서
                    int.TryParse(temp, out selection); //숫자로 변경
                } while (selection < 1|| selection > 3); // 1 ~ 3 사이가 아니면 while을 계속 실행 (1 ~ 3 일때만 종료)

                //shitch문을 통해 1,2,3을 처리
                switch (selection)
                {
                    case 1:
                        player.Attack(enemy);       //palyer가 enemy를 공격
                        break;
                    case 2:
                        player.HumanSkill(enemy);   //player이 enemy에게 스킬을 사용
                        break;
                    case 3:
                        player.Defense();           //3이면 방어 태세
                        break;
                    default:    // 1,2,3이 아닌 경우 실행,/  코드 구조상 들어오면 안된다.
                        break;
                }

                player.TestPrintStatus();   //나의 상태 출력
                enemy.TestPrintStatus();    //적의 상태 출력
                if (enemy.IsDead)   //적이 죽으면 승리표시를 하고 무한루프 종료
                {
                    Console.WriteLine("승리!");
                    break;
                }
                enemy.Attack(player); //적이 안죽었으면 적이 공격 시작
                player.TestPrintStatus();   //나의 상태 출력
                enemy.TestPrintStatus();    //적의 상태 출력
                if (player.IsDead)  //player이 죽으면 패배를 출력하고 무한루프 종료
                {
                    Console.WriteLine("패배");
                    break;
                }
            }







            //Human player = new Human();
            //Orc enemy = new Orc("오크");

            //while (player.HP >= 0 && enemy.HP >= 0)
            //{
            //    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            //    Console.WriteLine("┃        ※ 행동을 입력하세요            ┃   ");        
            //    Console.WriteLine("┃    ① 공격     ② 스킬     ③ 방어     ┃   ");
            //    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
            //    string a = Console.ReadLine();
            //    int.TryParse(a, out num);
            //    switch (num)
            //    {
            //        case 1:
            //            player.Attack(enemy);
            //            break;
            //        case 2:
            //            player.HumanSkill(enemy);
            //            break;
            //        case 3:
            //            player.Barrier = true;
            //            break;
            //        default:
            //            Console.WriteLine("다시 입력해주세요");
            //            continue;
            //    }
            //    player.TestPrintStatus();
            //    enemy.TestPrintStatus();
            //    if (enemy.HP <= 0 )
            //    {
            //        break;
            //    }
            //    enemy.Attack(player);       //orc공격

            //    player.TestPrintStatus();
            //    enemy.TestPrintStatus();
            //}
            



            Console.ReadKey();                  // 키 입력 대기하는 코드
        }   // Main 함수의 끝

    }
}
