# Menu

### Especificação do Problema
Alfred é dono de um restaurante e deseja gerenciar a produção dos pratos de seu restaurante de forma a
maximizar seu lucro. Alfred deseja planejar o que cozinhar nos próximos dias. Ele pode cozinhar vários
pratos. Para cada prato, o custo dos ingredientes e o lucro final é conhecido. Se um prato é cozinhado duas
vezes seguidas, o valor do lucro na segunda vez é **50** porcento do lucro na primeira vez. Se ele é preparado
uma terceira vez ou mais em seguida, o valor do lucro é **zero**. Por exemplo, se um prato, que gera um lucro
**v**, é cozinhado três vezes em seguida, o lucro final desses três dias é **1.5v**. Ajude-o a construir o cardápio que
maximiza o lucro sob a restrição de que seu orçamento não seja excedido.

### Entrada
A entrada consiste de vários casos de teste. Cada caso de teste começa com **3** inteiros em uma linha: O
número de dias **k(1 ≤ k ≤ 21)** que Alfred quer planejar, o número de pratos **n(1 ≤ n ≤ 50)** que ele pode
cozinhar e seu orçamento **m(0 ≤ m ≤ 100)**. As **n** próximas linhas descrevem os pratos que Alfred pode
cozinhar. A **i**-ésima linha contém dois inteiros: o custo **c(1 ≤ c ≤ 50)** e o lucro **v(1 ≤ v ≤ 10000)** do **i**-ésimo
prato.
O final da entrada é definido pelo caso de teste com **k = n = m = 0**. Não é necessário processar esse
caso de teste.

### Saída
Para cada saída, imprima o valor máximo do lucro alcançável, com **1** dígito após o ponto decimal. Imprima
então **k** inteiros com o **i**-ésimo inteiro sendo o número do prato a ser cozinhado no dia **i**. Pratos são numerados
de **1** a **n**. Imprima pelo menos um espaço ou caractere de nova linha após cada inteiro.
Se existirem vários cardápios possíveis alcançando o lucro máximo, selecione aquele com menor custo. Se
existirem dois ou mais com o mesmo custo mínimo, você pode imprimir qualquer um deles.
Se todo cardápio exceder o orçamento, imprima apenas o valor **0** como lucro.
A saída não deve ser escrita em nenhum arquivo. Ela deve ser escrita na saída padrão.

### Exemplo
* **Entrada**    
  
        2 1 5  
    
        3 5
    
        3 5 20
    
        2 5
    
        18 6
    
        1 1
    
        3 3
    
        2 3
    
        0 0 0
    
* **Saída**

        0.0
        
        13.0
        
        1 5 1

##### 1. Como esse problema pode ser modelado para a técnica Algoritmo Guloso?
Similar o Problema da Mochila, a resolução do Problema do Menu pode ser modelada buscando sempre o item que possui a melhor razão entre o valor e o custo. No contexto do Problema do Menu, cada prato possui um preço e custo de preparo. Quanto maior o custo-benefício desse prato, melhor o prato.

Os pratos perdem o valor a medida que são cozinhados mais de uma vez. Sendo assim, a razão entre valor e custo do prato precisa ser sempre atualizada. 

##### 2. Seu Algoritmo Guloso dá a solução ótima? Por quê?

##### 3. Como esse problema pode ser modelado para o paradigma de Programação Dinâmica?

##### 4. Discuta a sub-estrutura ótima e a sobreposição dos problemas.

##### 5. Se algum algoritmo clássico foi adaptado para resolver o problema, qual foi ele?
Similar o Problema da Mochila, com a resolução buscando sempre a solução que possui a melhor razão entre o valor e o custo. 
##### 6. Como sua implementação se comporta em termos de tempo de execução e memória alocada? Apresente uma variação experimental da solução, com entradas de tamanho diferente tentando, pelo menos, discutir também o melhor e o pior caso.

### Integrantes
* Pedro Augusto Duarte de Almeida
* Igor Octaviano Ribeiro Rezende
* Gabriel Silas do Carmo
* Victor Hugo Batista Medeiros
* Danilo Oliveira de Andrade
