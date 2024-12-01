import React, { useState, useEffect, forwardRef, useImperativeHandle } from "react";
import AcoesButton from "./AcoesButton";
import axios from "axios";
import apiConfig from '../Config/apiConfig';
import ExcluirProfissionalModal from "./ExcluirProfissonalModal";
import EditarProfissionalModal from "./EditarProfissionalModal";
import './css/ProfissionaisTable.css';

const ProfissionaisTable = forwardRef(({ filter , alertCallback },ref ) => {
  const [profissionais, setProfissionais] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);
  const itemsPerPage = 10;
  const [showExcluirModal, setShowExcluirModal] = useState(false);
  const [showEditarModal, setShowEditarModal] = useState(false);
  const [profissionalSelecionado, setProfissionalSelecionado] = useState(null);

  useEffect(() => {
    fetchProfissionais();
  }, [filter, currentPage]);

  const fetchProfissionais = async () => {
    try {

      const url = filter
        ? `/api/Profissional/getProfissionaisByEspecialidade?especialidadeId=${filter}`
        : `/api/Profissional/getProfissionais`;

        const { data } = await axios.get(apiConfig.baseURL + url);

      if (data.success) {
        const paginatedData = data.profissionais.slice(
            (currentPage - 1) * itemsPerPage,
            currentPage * itemsPerPage
          );
        setProfissionais(paginatedData);
        setTotalPages(Math.ceil(data.profissionais.length / itemsPerPage));
      } else {
        setProfissionais([]);
      }
    } catch (error) {
      console.error("Erro ao buscar profissionais:", error);
    }
  };


  const handlePageChange = (page) => {
    setCurrentPage(page);
  };

  const handleEdit = (profissional) => {
    setProfissionalSelecionado(profissional);
    setShowEditarModal(true);
  };

  const handleDelete = (profissional) => {    
    setProfissionalSelecionado(profissional);
    setShowExcluirModal(true);
  };

  const handleSaveEdit = async (updatedProfissional) => {
    try {

      const url = `${apiConfig.baseURL}/api/Profissional/EditarProfissional?` +
            `Id=${updatedProfissional.id}&` +
            `Nome=${encodeURIComponent(updatedProfissional.nome)}&` +
            `NumeroDocumento=${encodeURIComponent(updatedProfissional.numeroDocumento)}&` +
            `IdEspecialidade=${updatedProfissional.especialidadeId}`;
      
      console.log("url");
      console.log(url);

      const { data } = await axios.put(url);

      if (data.success) {
        alertCallback("Profissional editado com sucesso.", "success");
        fetchProfissionais();
      } else {
        alertCallback("Erro ao editar o profissional.", "error");
      }

    } catch (error) {
      alertCallback("Erro ao editar o profissional.", "error");
      
    } finally {
      setShowEditarModal(false);
      setProfissionalSelecionado(null);
    }
  };

  const confirmDelete = async () => {
    try {
      const url = `${apiConfig.baseURL}/api/Profissional/ExcluirProfissional?Id=${profissionalSelecionado.id}`;
      const { data } = await axios.delete(url);
  
      if (data.success) {
        alertCallback("Profissional excluído com sucesso.", "success");
        fetchProfissionais();
      } else {
        alertCallback("Erro ao excluir o profissional.", "error");
      }
    } catch (error) {
      alertCallback("Erro ao excluir o profissional.", "error");
    } finally {
      setShowExcluirModal(false);
      setProfissionalSelecionado(null);
    }
  };

  const handleModalClose = () => {
    setShowExcluirModal(false);
    setShowEditarModal(false);
    setProfissionalSelecionado(null);
  };

  useImperativeHandle(ref, () => ({
    refreshTable: fetchProfissionais,
  }));

  return (
    <div>
      <table className="table table-striped table-bordered table-hover">
        <thead>
          <tr>
            <th className="column-id">#</th>
            <th className="column-nome">Nome</th>
            <th className="column-especialidade">Especialidade</th>
            <th className="column-tpDoc">Tipo do Documento</th>
            <th className="column-numDoc">Número do Documento</th>
            <th className="column-acoes">Ações</th>
          </tr>
        </thead>
        <tbody>
          {profissionais.map((profissional) => (
            <tr key={profissional.id}>
              <td>{profissional.id}</td>
              <td>{profissional.nome}</td>
              <td>{profissional.especialidade}</td>
              <td>{profissional.tipoDocumento}</td>
              <td>{profissional.numeroDocumento}</td>
              <td>
                <AcoesButton
                    onEdit={() => handleEdit(profissional)}
                    onDelete={() => handleDelete(profissional)}
                />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    
      {totalPages > 1 && (
      <nav aria-label="Page navigation">
        <ul className="pagination justify-content-center">
          <li className={`page-item ${currentPage === 1 ? "disabled" : ""}`}>
            <button
              className="page-link"
              onClick={() => handlePageChange(currentPage - 1)}
              aria-label="Previous"
              disabled={currentPage === 1}
            >
              <span aria-hidden="true">&laquo;</span>
            </button>
          </li>
          {[...Array(totalPages).keys()].map((_, index) => (
            <li
              key={index + 1}
              className={`page-item ${index + 1 === currentPage ? "active" : ""}`}
            >
              <button
                className="page-link"
                onClick={() => handlePageChange(index + 1)}
              >
                {index + 1}
              </button>
            </li>
          ))}
          <li className={`page-item ${currentPage === totalPages ? "disabled" : ""}`}>
            <button
              className="page-link"
              onClick={() => handlePageChange(currentPage + 1)}
              aria-label="Next"
              disabled={currentPage === totalPages}
            >
              <span aria-hidden="true">&raquo;</span>
            </button>
          </li>
        </ul>
      </nav>
      )}

      

      <ExcluirProfissionalModal
        show={showExcluirModal}
        onClose={handleModalClose}
        onConfirm={confirmDelete}
        profissional={profissionalSelecionado}
      />

      <EditarProfissionalModal
        show={showEditarModal}
        onClose={handleModalClose}
        onSave={handleSaveEdit}
        profissional={profissionalSelecionado}
      />

    </div>
  );
})
;

export default ProfissionaisTable;
