$(document).ready(function () {

  // Listado (si usas server-side rendering no hace falta llamar a listar)
  // Listar solo si cargas por AJAX; si estás renderizando desde Razor, este bloque puede omitirse.
  // cargarTrabajadores();

  // Crear
  $('#formNuevo').on('submit', function (e) {
    e.preventDefault();
    var fd = new FormData(this);
    $.ajax({
      url: '/Trabajadores/Crear',
      type: 'POST',
      data: fd,
      contentType: false,
      processData: false,
      success: function (res) {
        alert(res.message || 'Guardado');
        $('#modalNuevo').modal('hide');
        location.reload();
      },
      error: function (err) {
        console.error(err);
        alert('Error al guardar');
      }
    });
  });

  // Obtener y mostrar modal de edición
  $(document).on('click', '.btnEditar', function () {
    var id = $(this).data('id');
    $.get('/Trabajadores/Obtener', { id: id }, function (t) {
      $('#editId').val(t.id);
      $('#editNombres').val(t.nombres);
      // ... más asignaciones ...
      $('#modalEditar').modal('show');
    }).fail(function () {
      alert('Error al obtener datos');
    });
  });

  // Editar
  $('#formEditar').on('submit', function (e) {
    e.preventDefault();
    var fd = new FormData(this);
    $.ajax({
      url: '/Trabajadores/Editar',
      type: 'POST',
      data: fd,
      contentType: false,
      processData: false,
      success: function (res) {
        alert(res.message);
        $('#modalEditar').modal('hide');
        location.reload();
      },
      error: function (err) {
        console.error(err);
        alert('Error al actualizar');
      }
    });
  });

  // Eliminar
  $(document).on('click', '.btnEliminar', function () {
    if (!confirm('¿Está seguro de eliminar este registro?')) return;
    var id = $(this).data('id');
    $.post('/Trabajadores/Eliminar', { id: id }, function (res) {
      alert(res.message);
      location.reload();
    }).fail(function () { alert('Error al eliminar'); });
  });

  // Filtro (si usas server-side rendering con query-string)
  $('#filtroSexo').on('change', function () {
    var sexo = $(this).val();
    var url = sexo ? '/Trabajadores?sexo=' + encodeURIComponent(sexo) : '/Trabajadores';
    window.location.href = url;
  });

});
