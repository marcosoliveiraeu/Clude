import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import axios from "axios";
import apiConfig from '../Config/apiConfig';
import "./css/ModalCustom.css";

const AddProfessionalModal = ({ onClose }) => {
  const [nome, setNome] = useState('');
  const [especialidades, setEspecialidades] = useState([]);
  const [selectedEspecialidade, setSelectedEspecialidade] = useState('');
  const [tipoDocumento, setTipoDocumento] = useState('');
  const [numeroDocumento, setNumeroDocumento] = useState('');
  const [errors, setErrors] = useState({});

  
  useEffect(() => {
    // Carrega especialidades do sessionStorage
    const storedEspecialidades = sessionStorage.getItem('especialidades');
    if (storedEspecialidades) {
      setEspecialidades(JSON.parse(storedEspecialidades));
    }
  }, []);

  

  const validateFields = () => {
    const newErrors = {};

    if (!nome || nome.length < 4 || nome.length > 100) {
      newErrors.nome = 'Nome deve ter entre 4 e 100 caracteres.';
    }
    if (!selectedEspecialidade) {
      newErrors.especialidade = 'Especialidade é obrigatória.';
    }
    if (!numeroDocumento || numeroDocumento.length < 3 || numeroDocumento.length > 50) {
      newErrors.numeroDocumento = 'Número do Documento deve ter entre 3 e 50 caracteres.';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSave = async () => {

    try{
        if (!validateFields()) {
            return;
        }

        const response = await axios.post(
            `${apiConfig.baseURL}/api/Profissional/IncluirProfissional`,
            null, 
            {
                params: {
                    Nome: nome,
                    NumeroDocumento: numeroDocumento,
                    IdEspecialidade: selectedEspecialidade,
                },
            }
        );

        if (response.data.success) {
            onClose(true, `Sucesso ao salvar profissional com id = ${response.data.profissional.id}`, 'success');

        } else {
            onClose(false, 'Houve um problema ao salvar o profissional.', 'error');
        }
    
    } catch (error) {
        onClose(false, 'Houve um problema ao salvar o profissional.', 'error');
        console.error("Erro ao salvar profissional:", error);
    }
  };

  const handleEspecialidadeChange = (especialidadeId) => {    
    const id = Number(especialidadeId); 
    setSelectedEspecialidade(id);
    const selected = especialidades.find((e) => e.id === id);
    setTipoDocumento(selected?.tipoDocumento || '');
  };

  return (

    <div className="modal-overlay-custom">
      <div className="modal-content-custom">

        <header className="modal-header-custom">
          <h5>Adicionar Profissional</h5>
          <button type="button" className="btn-close"  onClick={onClose}></button>         
        </header>

        <div className="modal-body-custom">
          <div className="form-group-custom">
            <label>Nome do Profissional</label>
            <input
              type="text"
              className={`form-control ${errors.nome ? 'is-invalid' : ''}`}
              value={nome}
              onChange={(e) => setNome(e.target.value)}
            />
            {errors.nome && <div className="invalid-feedback">{errors.nome}</div>}
          </div>

          <div className="form-group-custom">
            <label>Especialidade</label>
            <select
              className={`form-select ${errors.especialidade ? 'is-invalid' : ''}`}
              value={selectedEspecialidade}
              onChange={(e) => handleEspecialidadeChange(e.target.value)}
            >
              <option value="">Selecione uma especialidade</option>
              {especialidades.map((especialidade) => (
                <option key={especialidade.id} value={especialidade.id}>
                  {especialidade.nome}
                </option>
              ))}
            </select>
            {errors.especialidade && <div className="invalid-feedback">{errors.especialidade}</div>}
          </div>

          <div className="form-group-custom">
            <label>Tipo do Documento</label>
            <input
              type="text"
              className="form-control"
              value={tipoDocumento}
              readOnly
            />
          </div>

          <div className="form-group-custom">
            <label>Número do Documento</label>
            <input
              type="text"
              className={`form-control ${errors.numeroDocumento ? 'is-invalid' : ''}`}
              value={numeroDocumento}
              onChange={(e) => setNumeroDocumento(e.target.value)}
            />
            {errors.numeroDocumento && <div className="invalid-feedback">{errors.numeroDocumento}</div>}
          </div>
        </div>

        <div className="modal-footer-custom">
        
          <button type="button" className="btn btn-secondary" onClick={onClose}>Cancelar</button>
        
          <button className="btn btn-primary" onClick={handleSave}>
            Salvar
          </button>
        
        </div>


      </div>
    </div>
  );
};

AddProfessionalModal.propTypes = {
  onClose: PropTypes.func.isRequired,
};

export default AddProfessionalModal;
