#include <stdio.h>
#include <stdlib.h>

typedef struct list
{
    int data;
    struct list* next;
} list;

typedef struct node
{
    int data;
    struct list* next;
} node;

void runThrough(list* L);
void runThrough2(list** L);
list* createElement(int data, list* next);
list* createListFromArray(int* arr, int n);
void printList(list* lst);
void freeList(list** lst);

int main()
{

    list* lst = NULL;

    int arr[] = { 3, 6, 1, 9, 8, 4, 5 };
    lst = createListFromArray(arr, 7);
    node* nde;
    nde = createElement(0, lst);
    printf("before (no change):\n");
    printList(nde);

    //1
    runThrough(nde);
    printf("after 1 (lst pointer, *nde):\n");
    printList(nde);

    //2
    runThrough2(&nde);
    printf("after 2 (lst is pointer to pointer ,**nde):\n");
    printList(nde);
    // write output:

    // free lists:
    freeList(&lst);
    freeList(&nde);

    return 0;
}
list* createElement(int data, list* next)
{
    list* item = (list*)(malloc(sizeof(list)));
    item->data = data;
    item->next = next;
    return item;
}

list* createListFromArray(int* arr, int n)
{
    int i;
    list* head = NULL;
    for (i = n - 1; i >= 0; i--)
        head = createElement(arr[i], head);
    return head;
}

void printList(list* lst)
{
    // your code:
    int first = 1;
    if(!lst)
        printf("List is empty");
    while (lst){
        if (first){
            printf("%d", lst->data);
            first = 0;
        } else {
            printf(" -> %d", lst->data);
        }
        lst = lst -> next;
    }
    printf("\n");
}
// --------------------------- //
void freeList(list** lst)
{
    // your code:
    list* toFree = *lst;
    while(toFree){
        *lst = (*lst) -> next;
        free(toFree);
        toFree = *lst;
    }
    *lst = NULL;
}

void runThrough(list* L){
    while(L != NULL){
        L = L->next;
    }
}
void runThrough2(list** L){
    while(*L != NULL){
        *L = (*L)->next;
    }
}