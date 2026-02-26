# ?? GUÍA DE PRUEBAS - FORM3 VENTAS

## Antes de realizar las pruebas, asegúrate de:

1. ? Compilar la solución (Ya está compilada y sin errores)
2. ? Los procedimientos almacenados están creados en la base de datos
3. ? La conexión a la base de datos está correcta

---

## ?? CASOS DE PRUEBA

### Prueba 1: Verificar que se cargan las ventas registradas
**Pasos:**
1. Abre Form3 (Ventas)
2. Observa el DataGrid en la parte superior

**Resultado Esperado:**
- ? El DataGrid debe mostrar todas las ventas registradas (si existen)
- ? Las columnas deben mostrar: Fecha_venta, Cliente_Nombre, Cliente_Telefono, Cliente_Direccion, Total_general, Estado_venta
- ? Si no hay ventas, debe mostrar mensaje: "No hay ventas registradas"

**Resultado Anterior (Incorrecto):**
- ? El DataGrid no mostraba nada
- ? Siempre mostraba: "No hay ventas registradas"

---

### Prueba 2: Crear una nueva venta
**Pasos:**
1. Desde Form3, selecciona un cliente en comboBox1
2. Haz clic en el botón "Crear" (button1)
3. Observa el resultado

**Resultado Esperado:**
- ? Se debe crear una nueva venta
- ? Debe mostrar mensaje: "Nueva venta creada exitosamente para el cliente: [nombre del cliente]"
- ? El DataGrid se debe refrescar automáticamente y mostrar la nueva venta
- ? La venta debe aparecer en la parte superior del listado (orden descendente por fecha)

**Resultado Anterior (Incorrecto):**
- ? La venta se creaba pero el DataGrid no se refrescaba
- ? Tenías que cerrar y abrir el formulario de nuevo para ver la venta

---

### Prueba 3: Añadir un producto a una venta
**Pasos:**
1. Desde Form3, selecciona una venta en el DataGrid
2. Selecciona un producto en comboBox2
3. Ingresa una cantidad en textBox1 (por ejemplo: 5)
4. Haz clic en el botón "Añadir" (button2)
5. Observa el resultado

**Resultado Esperado:**
- ? Se debe añadir el producto a la venta
- ? Debe mostrar mensaje: "Producto '[nombre]' (Cantidad: [cantidad]) añadido a la venta exitosamente"
- ? El DataGrid se debe refrescar automáticamente
- ? El campo de cantidad (textBox1) debe limpiar su contenido

**Resultado Anterior (Incorrecto):**
- ? A veces no se añadía correctamente
- ? El DataGrid no se refrescaba

---

### Prueba 4: Cargar clientes correctamente
**Pasos:**
1. Desde Form3, observa el comboBox1 (Clientes)
2. Haz clic en el comboBox1 para ver la lista

**Resultado Esperado:**
- ? Debe mostrar todos los clientes registrados
- ? Al crear una venta, debe estar disponible al menos un cliente
- ? Al abrir el formulario, debe mostrar mensaje: "? Se cargaron [número] clientes correctamente"

**Resultado Anterior:**
- ?? Mostraba todos los clientes, pero con redundancia de código

---

### Prueba 5: Cargar productos correctamente
**Pasos:**
1. Desde Form3, observa el comboBox2 (Productos)
2. Haz clic en el comboBox2 para ver la lista

**Resultado Esperado:**
- ? Debe mostrar todos los productos registrados
- ? Cada producto debe tener su nombre, categoría y precio

**Resultado Anterior:**
- ?? Funcionaba, pero con redundancia de código en Form2

---

### Prueba 6: Eliminar una venta
**Pasos:**
1. Desde Form3, selecciona una venta en el DataGrid
2. Haz clic en el botón "Eliminar" (buttonEliminar)
3. Confirma la eliminación en el diálogo
4. Observa el resultado

**Resultado Esperado:**
- ? Se debe desactivar la venta
- ? Debe mostrar mensaje: "Venta eliminada correctamente"
- ? El DataGrid se debe refrescar automáticamente
- ? La venta no debe aparecer más en el listado

---

## ?? VERIFICACIÓN DE REDUNDANCIA DE CÓDIGO

### Redundancia eliminada 1: Form3.cs
**Antes:**
```csharp
private Detalle_VentasBL detalleVentasBL; // Variable privada sin usar correctamente
detalleVentasBL = new Detalle_VentasBL(); // Se instanciaba en InicializarComponentes
var ventaDetalleBL = new Detalle_VentasBL(); // Se instanciaba de nuevo en CargarVentas
```

**Después:**
```csharp
// Se eliminó la variable privada
// Se crea una instancia local solo donde se necesita (button2_Click)
var detalleVentasBL = new Detalle_VentasBL();
```

### Redundancia eliminada 2: Form2.cs
**Antes:**
```csharp
private ProductosBL productosBL; // Variable privada
private void CargarProductos()
{
    var productosDetalleBL = new ProductosBL(); // Instancia redundante
    var productos = productosDetalleBL.Listar();
}
```

**Después:**
```csharp
private ProductosBL productosBL; // Variable privada
private void CargarProductos()
{
    var productos = productosBL.Listar(); // Usa la variable privada
}
```

---

## ?? MATRIZ DE PROCEDIMIENTOS ALMACENADOS

| Operación | Procedimiento | DAL | BL | Formulario |
|-----------|---------------|-----|----|------------|
| Listar Ventas | sp_ListarVentasConCliente | VentasDAL | VentasBL | Form3 |
| Crear Venta | sp_insertar_venta | VentasDAL | VentasBL | Form3 |
| Desactivar Venta | sp_desactivar_venta | VentasDAL | VentasBL | Form3 |
| Listar Clientes | sp_ListarClientes | ClienteDAL | ClienteBL | Form3 |
| Listar Productos | sp_listar_productos | ProductoDAL | ProductosBL | Form2, Form3 |
| Listar Categorías | sp_listar_categoria | CategoriaDAL | CategoriaBL | Form1, Form2 |
| Insertar Detalle Venta | sp_insertar_detalle_venta | Detalle_VentasDAL | Detalle_VentasBL | Form3 |

---

## ? CHECKLIST DE PRUEBAS

- [ ] Form3 carga las ventas correctamente al abrir
- [ ] Se pueden crear nuevas ventas
- [ ] El DataGrid se refresca después de crear una venta
- [ ] Se pueden añadir productos a una venta
- [ ] El DataGrid se refresca después de añadir un producto
- [ ] Se pueden eliminar ventas
- [ ] El DataGrid se refresca después de eliminar una venta
- [ ] Form2 carga los productos sin redundancia
- [ ] No hay errores en la consola de Visual Studio
- [ ] La compilación es exitosa sin advertencias

---

## ?? PRÓXIMOS PASOS (Opcional)

1. **Implementar cálculo de Total General**: Actualmente se inserta como 0. Podría calcularse sumando los subtotales de los detalles.
2. **Refrescar datos solo cuando sea necesario**: En lugar de cargar todos los productos cada vez, podrías cachearlos.
3. **Agregar validación de stock**: Verificar que el stock disponible sea suficiente antes de añadir un producto.
4. **Implementar actualización de ventas**: Actualmente está marcado como "Funcionalidad pendiente".

---

**Documento de Pruebas - Estado: ? LISTO PARA PROBAR**
