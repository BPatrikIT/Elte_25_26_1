#include <iostream>
#include <vector>

int main() {
    std::vector<int> x = {1, 2, 3, 4, 5, 6, 7};
    x.push_back(10);
    std::cout << "Hello World\n";
    std::cout << x[0] << '\n' << x.size() << '\n';
    x[0] = 20;
    std::cout << x[0] << '\n' << x.size() << '\n';
    //printf("Hello World!");

    
    for (int i = 0; i < 10; i++)
    {
        std::cout << i << ' ';
    }

    std::cout << '\n';

    for (int i : x) {
        std::cout << i << ' ';
    }
    std::cout << '\n';

    for(std::vector<int>::iterator i = x.begin(); i != x.end(); ++i){
        std::cout << *i << ' ';
    }
    std::cout << '\n';

    if (x[0] == 10)
    {
        std::cout << 10;
    } else if (x[0] == 20)
    {
        std::cout << 20;
    }else
    {
        std::cout << 'x';
    }

    class Fibonacci {
    public:
        Fibonacci();
        int current() const { return curr; }
        int operator++(int) {
            int tmp = curr + prev;
            prev = curr;
            curr = tmp;
            return tmp;
        }
        int operator++() {
            int tmp = curr + prev;
            prev = curr;
            curr = tmp;
            return prev;
        }
        
        private:
            int curr, prev;
    };

    Fibonacci f;
    std::cout << f.current() << '\n';
    for (size_t i = 0; i < 10; i++)
    {
        
    }
    


    return 0;
}