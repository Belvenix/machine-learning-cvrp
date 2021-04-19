# machine-learning-cvrp
Algorytm genetyczny został zaimplementowany na potrzeby zajęć z Uczenia Maszynowego na Politechnice Wrocławskiej. 
Struktura kodu jest wzorowana na tej, która została przedstawiona na laboratoriach z Metaheurystyk.

Celem projektu jest zbadanie jak hiper parametry algorytmu genetycznego wpływają na jego jakość. Parametry które są rozważane to:

1. Prawdopodobieństwo krzyżowania (operator Ordered Crossover)
2. Prawdopodobieństwo mutacji (operator Two-Point Swapping)
3. Ilość osobników
4. Ilość generacji
5. Ilość osobników w turnieju

Pseudokod przedstawia działanie algorytmu genetycznego
```C#
public void AG(){
  Initialize();
  
  while(!ShouldStop){
    Select();
    
    Crossover();
    Evaluate();
    
    bool foundNewBest = CheckNewBest();
    
    Mutate();
    Evaluate();
    
    CheckNewBest();
  }
}
```

Do porównania zostały zaimplementowane losowe wyszukanie jak i algorytm zachłanny.

Problem rozważany w tym projekcie to Travelling Salesman Problem tj. problem komiwojażera. 
Jednakże kod pozwala na rozwinięcie do problemu Capacitated Vehicle Routing Problem.
