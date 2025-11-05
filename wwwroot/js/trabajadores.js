const apiUrl = "/api/trabajadores";
const tbody = document.getElementById("tbodyTrabajadores");
const modal = new bootstrap.Modal(document.getElementById("modalTrabajador"));
const form = document.getElementById("formTrabajador");
const filtroSexo = document.getElementById("filtroSexo");
const btnNuevo = document.getElementById("btnNuevo");

document.addEventListener("DOMContentLoaded", listarTrabajadores);
filtroSexo.addEventListener("change", listarTrabajadores);
btnNuevo.addEventListener("click", abrirModalNuevo);
form.addEventListener("submit", guardarTrabajador);

// 🔹 Listar trabajadores
function listarTrabajadores() {
  const sexo = filtroSexo.value;
  fetch(`${apiUrl}?sexo=${sexo}`)
    .then(res => res.json())
    .then(data => {
      tbody.innerHTML = "";
      data.forEach(t => {
        const tr = document.createElement("tr");
        tr.className = t.sexo === "Masculino" ? "table-primary" : "table-warning";
        tr.innerHTML = `
          <td>${t.id}</td>
          <td>${t.nombres}</td>
          <td>${t.apellidos}</td>
          <td>${t.tipoDocumento} ${t.numeroDocumento}</td>
          <td>${t.sexo}</td>
          <td>${t.fechaNacimiento?.split("T")[0]}</td>
          <td>${t.direccion}</td>
          <td>
            <button class="btn btn-sm btn-info me-2" onclick="editarTrabajador(${t.id})">Editar</button>
            <button class="btn btn-sm btn-danger" onclick="eliminarTrabajador(${t.id})">Eliminar</button>
          </td>
        `;
        tbody.appendChild(tr);
      });
    });
}

// 🔹 Abrir modal para nuevo registro
function abrirModalNuevo() {
  form.reset();
  document.getElementById("id").value = "";
  document.getElementById("modalLabel").textContent = "Nuevo Trabajador";
  modal.show();
}

// 🔹 Guardar (nuevo o editar)
function guardarTrabajador(e) {
  e.preventDefault();
  const id = document.getElementById("id").value;
  const trabajador = {
    id: id || 0,
    nombres: document.getElementById("nombres").value,
    apellidos: document.getElementById("apellidos").value,
    tipoDocumento: document.getElementById("tipoDocumento").value,
    numeroDocumento: document.getElementById("numeroDocumento").value,
    sexo: document.getElementById("sexo").value,
    fechaNacimiento: document.getElementById("fechaNacimiento").value,
    direccion: document.getElementById("direccion").value,
    foto: null
  };

  const method = id ? "PUT" : "POST";
  const url = id ? `${apiUrl}/${id}` : apiUrl;

  fetch(url, {
    method,
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(trabajador)
  })
    .then(res => {
      if (!res.ok) throw new Error("Error en la operación");
      modal.hide();
      listarTrabajadores();
    })
    .catch(err => alert(err.message));
}

// 🔹 Editar
function editarTrabajador(id) {
  fetch(`${apiUrl}/${id}`)
    .then(res => res.json())
    .then(t => {
      document.getElementById("id").value = t.id;
      document.getElementById("nombres").value = t.nombres;
      document.getElementById("apellidos").value = t.apellidos;
      document.getElementById("tipoDocumento").value = t.tipoDocumento;
      document.getElementById("numeroDocumento").value = t.numeroDocumento;
      document.getElementById("sexo").value = t.sexo;
      document.getElementById("fechaNacimiento").value = t.fechaNacimiento.split("T")[0];
      document.getElementById("direccion").value = t.direccion;

      document.getElementById("modalLabel").textContent = "Editar Trabajador";
      modal.show();
    });
}

// 🔹 Eliminar
function eliminarTrabajador(id) {
  if (!confirm("¿Está seguro de eliminar el registro?")) return;
  fetch(`${apiUrl}/${id}`, { method: "DELETE" })
    .then(res => {
      if (!res.ok) throw new Error("Error al eliminar");
      listarTrabajadores();
    })
    .catch(err => alert(err.message));
}
