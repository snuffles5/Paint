/**************************

                            Question 2 Advanced C 23/6/22.

***************************/

#include <stdio.h>
#include <stdlib.h>
typedef struct List {
    int num;
    struct List* next;
} List;

unsigned int getNumFromList(List* list){
    if (!list) return 0;
    int num = list -> num;
    list = list -> next;
    while(list != NULL){
        num = ((num*10)+list->num);
        list = list -> next;
    }
    return num;
}
// original
//unsigned int getNumFromList(List* list){
//    if (!list) return 0;
//    int num = list -> num;
//    list = list -> next;
//    while(!list){
//        num = ((num*10)+list->num);
//        list = list -> next;
//    }
//    return num;
//}


int getNumOfDigFromPosNum(unsigned int num){
    int count = 0;
    if (num < 10) return 1;
    for(count = 0; num > 0; count++)
        num = num / 10;
    return count;
}
int* getAllDigs(unsigned int n, int* size){
    *size = getNumOfDigFromPosNum(n);
    int *arr, i;
    arr = (int*) calloc (*size, sizeof(int));
    for(i = (*size)-1 ; i >= 0; i--){
        arr[i]   = n % 10;
        n = n / 10;
    }
    return arr;
}

//original
//void List_num_add(List* L, unsigned int N){
//    unsigned int M = getNumFromList(L);
//    unsigned int new = M + N;
//    int size, i;
//    int* arr = getAllDigs(new, &size);
//    for (i = 0; i<size; i++ , L = L -> next){
//        if(!L)
//            L = (List*) malloc (1* (sizeof (List)));
//        L -> num = arr[i];
//    }
//    L->next = NULL;
//    free(arr);
//}

void List_num_add(List* L, unsigned int N){
    // add head
    List* head = L;
    unsigned int M = getNumFromList(L);
    unsigned int new = M + N;
    int size, i;
    int* arr = getAllDigs(new, &size);
    for (i = 0; i<size; i++ , L = L -> next){
        // change L to L->next
        if(L -> next != NULL)
            L -> next = (List*) malloc (1* (sizeof (List)));
        L -> num = arr[i];
    }
    //delete
//    L->next = NULL;
    L = head;
    free(arr);
}


// works:
//void List_num_add(List* L, unsigned int N){
//    unsigned int M = getNumFromList(L);
//    unsigned int new = M + N;
//    List* head = L;// added
//    int size , i;
//    int* arr = getAllDigs(new, &size);
//    for (i = 0; i < size; i++ , L = L -> next){
//        L -> num = arr[i];
//        if(L->next == NULL && i < size-1)
//            L -> next = (List*) malloc (1* (sizeof (List)));
//    }
////    L->next = NULL; //original;
//    L = head;
//    free(arr);
//}

List* createElement(int data, List* next)
{
    List* item = (List*)(malloc(sizeof(List)));
    item->num = data;
    item->next = next;
    return item;
}
List* createLi(int* a, int n){
    List *head = NULL, *tail; int i;
    for (i  = 0; i < n; i++) {
        if (head == NULL)
            head = tail = createElement(a[i], NULL);
        else {
            tail->next = createElement(a[i], NULL);
            tail = tail->next;
        }
    }
    return head;
}

void printList(List* lst)
{
    // your code:
    int i;
    if (lst == NULL){
        printf("list is empty\n");
        return;
    }
    for (i = 0; lst != NULL; i++,lst = lst -> next) {
        if (i == 0)
            printf("%d", lst->num);
        else
            printf(" -> %d", lst->num);
    }
    printf("\n");
}

int main()
{
    List* lst = NULL;
    int a[] = {9,9,9,4};
    lst = createLi (a, 4);
    printList(lst);
    List_num_add(lst,18);
    printList(lst);
    return 0;
}