using System;
using System.Collections;
using System.Collections.Generic;

public interface IDAO<T>
{
    T Get(int id);

    List<T> GetAll();

    void Save(T t);

    void Update(T t);

    void Delete(T t);
}
