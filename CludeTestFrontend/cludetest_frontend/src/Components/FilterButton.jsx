import React, { useEffect, useState } from 'react';
import axios from 'axios';
import apiConfig from '../Config/apiConfig';
import './css/FilterButton.css';
import PropTypes from 'prop-types';

const FilterButton = ({ onFilterChange , onValidation }) => {
  const [especialidades, setEspecialidades] = useState([]);
  const [selectedFilter, setSelectedFilter] = useState('Todas');
  const [isLoading, setIsLoading] = useState(false);

  // Função para carregar as especialidades da API
  const fetchEspecialidades = async () => {
    setIsLoading(true);

    try {
      
      const cachedEspecialidades = sessionStorage.getItem('especialidades');

      if (cachedEspecialidades) {

        setEspecialidades(JSON.parse(cachedEspecialidades));

      } else {

        const response = await axios.get(`${apiConfig.baseURL}/api/Especialidade/getEspecialidades`);
        const data = response.data;

        if (data.success) {    

          const especialidadesApi = data.especialidades.map(e => ({
            id: e.id,
            nome: e.nome,
            tipoDocumento: e.tipoDocumento
          }));          

          // Salva no sessionStorage
          sessionStorage.setItem('especialidades', JSON.stringify(especialidadesApi));
          setEspecialidades(especialidadesApi);
          
          onValidation?.(true);

        } else {
          onValidation?.(false);
          console.error('Erro ao carregar especialidades:', data.message);
        }

      }       

    } catch (error) {
      console.error('Erro na chamada da API:', error);
      onValidation?.(false);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchEspecialidades();
  }, []);

  // Handler para quando uma especialidade for selecionada
  const handleSelect = (idEspecialidade, nomeEspecialidade) => {
    setSelectedFilter(nomeEspecialidade);
    onFilterChange(nomeEspecialidade === 'Todas' ? null : idEspecialidade);
  };

  return (
    <div className="dropdown">
      <button
        className="btn custom-btn btn-secondary dropdown-toggle "        
        type="button"
        id="dropdownMenuButton"
        data-bs-toggle="dropdown"
        aria-expanded="false"
      >
        Filtrar por: {selectedFilter}
      </button>
      <ul className="dropdown-menu" aria-labelledby="dropdownMenuButton">
        <li className="titulodropdown">FILTRAR POR:</li>
        <li>
          <button
            className="dropdown-item"
            onClick={() => handleSelect(0,'Todas')}
          >
            Todas
          </button>
        </li>
        {isLoading ? (
          <li className="dropdown-item">Carregando...</li>
        ) : (
          especialidades.map((especialidade) => (
            <li key={especialidade.id}>
              <button
                className="dropdown-item"
                onClick={() => handleSelect(especialidade.id,especialidade.nome)}
              >
                {especialidade.nome}
              </button>
            </li>
          ))
        )}
      </ul>
    </div>
  );
};

FilterButton.propTypes = {
  onFilterChange: PropTypes.func.isRequired,
  onValidation: PropTypes.func,
};

export default FilterButton;


