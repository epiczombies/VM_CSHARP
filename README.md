# VM_CSHARP
A Virtual Machine running code from the function < public void test() >
This will generate real Native C assembly but runs only with this executor.
# Made by Me
# Credit me if you use

the function test is following as a file.

int set_eax_to_2 = 2;
void WinMain()
{
  TEST(0);
}
void TEST(int a1)
{
  // you cant set the register directly so you need this
  __asm
  {
    mov eax, [set_eax_to_2]
  }
}
#if i had have time i would add a parser so it would look like if i would do my parser.

int set_eax_to_2 = 2;
void WinMain()
{
  TEST(0);
}
void TEST(int a1)
{
// this will do the same as Mov eax, [set_eax_to_2]  but this parser would allow you to do this
  FUNCTION.REGISTER.EAX = set_eax_to_2;
}
