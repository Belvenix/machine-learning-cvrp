# machine-learning-cvrp

For English, please scroll below.

## Polish

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

## English

The genetic algorithm was implemented for the purposes of the Machine Learning classes at the Wrocław University of Technology.
The code structure is based on the one presented in the laboratories of the Metaheuristics.

The aim of the project is to investigate how hyper parameters of the genetic algorithm influence its quality. The parameters that are considered are:

1. Probability of crossover (Ordered Crossover operator)
2. Probability of mutation (operator Two-Point Swapping)
3. Number of individuals
4. Number of generations
5. Number of players in the tournament

The pseudocode shows the operation of the genetic algorithm
```C#
public void AG () {
  Initialize ();
  
  while (! ShouldStop) {
    Select ();
    
    Crossover ();
    Evaluate ();
    
    bool foundNewBest = CheckNewBest ();
    
    Mutate ();
    Evaluate ();
    
    CheckNewBest ();
  }
}
```

A random search and a greedy algorithm were implemented for comparison.

The problem considered in this project is the Traveling Salesman Problem.
However, the code can be expanded to Capacitated Vehicle Routing Problem.
